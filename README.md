
# Hacker News Top Posts
 
 Get number of top post(s) from the Hacker News and convert into JSON by simple command line application.

This project is using this official hacker news api: [https://github.com/HackerNews/API](https://github.com/HackerNews/API)

Source: [https://blog.ycombinator.com/hacker-news-api/](https://blog.ycombinator.com/hacker-news-api/)

Example output:

**[  
{  
"title": "Web Scraping in 2016",  
"uri": "https://franciskim.co/2016/08/24/dont-need-no-stinking-api-web-scraping-2016-beyond/",  
"author": "franciskim",  
"points": 133,  
"comments": 80,  
"rank": 1  
},  
{  
"title": "Instapaper is joining Pinterest",  
"uri": "http://blog.instapaper.com/post/149374303661",  
"author": "ropiku",  
"points": 182,  
"comments": 99,  
"rank": 2  
}  
]**

## Build and Run without Docker

Open terminal and type 'cd' commnd to the root of project directory

**Publish on OSX**

Use the following commands on terminal: 
```
dotnet restore HackerNews/HackerNews.csproj

dotnet publish HackerNews/HackerNews.csproj --self-contained -f netcoreapp3.1 -r osx-x64 -o ../Build/OSX -c Release
```

**Publish on Windows**

Use the following commands on terminal:
```
dotnet restore HackerNews/HackerNews.csproj

dotnet publish HackerNews/HackerNews.csproj --self-contained -f netcoreapp3.1 -r win-x64 -o ../Build/Windows -c Release
```

**Publish on Linux**

Use the following commands on terminal: 

```
dotnet restore HackerNews/HackerNews.csproj

dotnet publish HackerNews/HackerNews.csproj --self-contained -f netcoreapp3.1 -r linux-x64 -o ../Build/OSX -c Release
```

**Run the app**

Open terminal and type 'cd' commnd to the bulid/operating directory after you have published the app then enter the following command
```
./hackernews --posts {enter number}
```

## Build and run with Docker
Credit: [https://docs.docker.com/engine/examples/dotnetcore/](https://docs.docker.com/engine/examples/dotnetcore/)
* Download and install [Docker](https://www.docker.com/products/docker-desktop)

Build and run the Docker image:
1.  Open a command prompt and navigate to your project folder.
2.  Use the following commands to build and run your Docker image:

```
$ docker build -t hackernews .
$ docker run -d -p 8080:80 hackernews app
```

## Prerequisites

Install the following prerequisites:

-   [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download)  
    If you have .NET Core installed, use the  `dotnet --info`  command to determine which SDK you're using.
    
-   [Docker Community Edition](https://www.docker.com/products/docker-desktop)
    
-   A temporary working folder for the  _Dockerfile_  and .NET Core example app. In this tutorial, the name  _docker-working_  is used as the working folder.


**Reasons why I use .Net Core 3.1:**
 *   Cross-platform
 *   Microservices compatibility.
 *   Docker compatibility.
 *   High-performance and scalable system.

**Libraries Used**
 * [Newtonsoft.Json v12.0.3](https://www.newtonsoft.com/json)
	
**Reasons why I use Newtonsoft.Json**
 *   Popular high-performance JSON framework for .NET
 * World-class JSON Serializer
 * Easy To Use
 * High Performance

## Running the unit tests

Open terminal and type 'cd' commnd to the root of project directory and enter the following command
```
dotnet test HackerNews.UnitTests
```
