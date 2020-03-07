using System;
using System.Collections.Generic;
using HackerNews.Interfaces;
using HackerNews.LineCommand;

namespace HackerNews
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create a new interface to keep main method clean and tidy
            ITypeCommand typeCommand = new PostCommand();

            //Info to viewer what to expect
            typeCommand.ShowInfo();

            try
            {
                //Run the post command
                typeCommand.ValidCommandLine(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }
}
