using System;
using System.Collections.Generic;
using System.Net;
using HackerNews.LineCommand;
using HackerNews.Models;
using Newtonsoft.Json;

namespace HackerNews.Interfaces
{
    //This is only for posts api command
    public class PostCommand : ITypeCommand
    {
        //Source: https://blog.ycombinator.com/hacker-news-api/
        //Document for API: https://github.com/HackerNews/API
        //You can chnage which format of JSON
        public string apiFormat = ".json?print=pretty";
        //API to get list of id of top 500 stores
        private string topStoriesURL = "https://hacker-news.firebaseio.com/v0/topstories";
        //To get the post details
        private string getItemUrl = "https://hacker-news.firebaseio.com/v0/item/";

        public PostCommand()
        {
        }

        public void ValidCommandLine(string[] args)
        {
            var count = 0;
            //If enter no arguments, then throw exception
            if (args == null)
            {
                throw new NullReferenceException("No command entered");
            }

            //Ensure the user enter two arguments
            if (args.Length != 2)
            {
                throw new ArgumentException("Only accept two arguments e.g. --posts 100");
            }

            var postArgument = args[0];
            var valueArgument = args[1];

            //Ensure either of two argument is not string empty
            if (string.IsNullOrEmpty(postArgument) || string.IsNullOrEmpty(valueArgument))
            {
                if (string.IsNullOrEmpty(postArgument)) throw new NullReferenceException("Please enter '--posts'");
                if (string.IsNullOrEmpty(valueArgument)) throw new NullReferenceException("Please enter postive number");
            }

            //Ensure the first argument is entered exactly like this "--posts"
            if (postArgument.ToLower().Trim() != "--posts")
            {
                throw new ArgumentException("Please enter '--posts'");
            }

            //Ensure the number is postive and is not over than 100
            if (int.TryParse(valueArgument, out var intNumber))
            {
                if (intNumber <= 0 || intNumber >= 100)
                {
                    throw new ArgumentException("Number must be up to 100");
                }

                count = intNumber;
            }
            else
            {
                throw new ArgumentException("Please enter a positive integer up to 100");
            }


            //If the post command line passed the test then get the posts into JSON
            CallHackerApi(count);
        }

        public void ShowInfo()
        {
            Console.WriteLine("\n(Will show top number of posts that have points, comments and rank over 1)");
            Console.WriteLine("\nPlease note: Will only fetch post which have comments, points and ranking over 0 and also have non empty string of title and author which not longer than 256 characters,");
        }

        public void CallHackerApi(int count)
        {
            //Ensure the url is available
            if (IsUrlAvailable(topStoriesURL + apiFormat))
            {
                try
                {
                    //Get top 500 stories' ID
                    var json = GetJson(topStoriesURL + apiFormat);
                    //Convert JSON into list
                    var listOfIDs = JsonConvert.DeserializeObject<IEnumerable<long>>(json);
                    GetPostToJson(listOfIDs, count);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
            else
            {
                throw new ArgumentException("Api connection is down. Please check your internet or try again later");
            }
        }

        public bool IsUrlAvailable(string url)
        {
            try
            {
                // From this answer: https://stackoverflow.com/a/7581824/700206
                Uri uriResult;
                bool tryCreateResult = Uri.TryCreate(url, UriKind.Absolute, out uriResult);
                if (tryCreateResult == true && uriResult != null)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public string GetJson(string url)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    //Read and download the content from URL
                    return wc.DownloadString(url);
                }
            }
            catch {
                throw new ArgumentException("Failed to get JSON result");

            }
        }

        public void GetPostToJson(IEnumerable<long> postIDs, int count)
        {
            try
            {
                var ranking = 1;
                var validPosts = new List<PostOutput>();

                foreach (var id in postIDs)
                {
                    //Get post details
                    var json = GetJson(getItemUrl + id.ToString() + apiFormat);
                    //Convert JSON to Post object
                    var post = JsonConvert.DeserializeObject<Post>(json);

                    //If the post have the following:
                    //1.title and author are non empty strings not longer than 256 characters.
                    //2.uri is a valid URI
                    //3.points, comments and rank are integers >= 0.

                    if (IsValidPost(post))
                    {
                        //If valid then convert into new custom post object for the output
                        validPosts.Add(new PostOutput
                        {
                            title = post.Title,
                            uri = post.Url,
                            author = post.By,
                            points = post.Score,
                            comments = post.Descendants,
                            rank = ranking,
                        });
                    }

                    ranking++;
                    //Once it reach the count from the command line then stop the foreach loop
                    if (validPosts.Count == count) break;
                }

                //Convert the list of custom object into pretty JSON
                Console.WriteLine(JsonConvert.SerializeObject(validPosts, Formatting.Indented));
            }
            catch (Exception ex) {
                throw new ArgumentException(ex.Message);
            } 
        }

        public bool IsValidPost(Post post)
        {
            if (string.IsNullOrEmpty(post.Title) || string.IsNullOrEmpty(post.By) || string.IsNullOrEmpty(post.Url)) return false;

            if (!IsUrlAvailable(post.Url)) return false;

            if (post.Score < 0 || (post.Kids == null || post.Kids.Count < 0)) return false;

            if (post.Title.Trim().Length > 256 || post.By.Trim().Length > 256) return false;

            return true;
        }

    }
}
