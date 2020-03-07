using System;
namespace HackerNews.LineCommand
{
    //Create this interface in case for other command for example to get comments, url or author api
    public interface ITypeCommand
    {
        void ValidCommandLine(string[] args);
        void ShowInfo();
    }
}
