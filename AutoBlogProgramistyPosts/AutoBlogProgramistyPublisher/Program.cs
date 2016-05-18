using AutoBlogProgramistyPosts;
using System;
using System.IO;
using System.Diagnostics;

namespace AutoBlogProgramistyPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!File.Exists(args[0]))
            {
                throw new Exception("Brak pliku {0} na dysku");
            }

            Console.WriteLine("Rozpoczęcie publikowania postu na WordPress-ie");

            var newsPublisher = new NewsPublisher(url =>
            {
                Process.Start(url);

                Console.WriteLine("Podaj klucz do autoryzacji Twittera");

                return Console.ReadLine();
            });

            newsPublisher.PublishNews(args[0]);

            Console.ReadKey();
        }
    }
}
