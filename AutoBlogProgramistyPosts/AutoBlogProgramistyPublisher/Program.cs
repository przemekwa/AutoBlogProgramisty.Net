using AutoBlogProgramistyPosts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using AutoBlogProgramistyPosts.PostCreators;
using AutoBlogProgramistyPosts.PostEngines;
using AutoBlogProgramistyPosts.Dto;

namespace AutoBlogProgramistyPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!File.Exists(args[0]))
            {
                throw new Exception("Brak pliku {0} na dysk");
            }

            Console.WriteLine("Rozpoczęcie publikowania postu na WordPress-ie");

            var newsPublisher = new NewsPublisher(url =>
            {
                Process.Start(url);

                Console.WriteLine("Podaj klucz do autoryzacji Twittera");

                return Console.ReadLine();
            });


            Console.ReadKey();
        }
    }
}
