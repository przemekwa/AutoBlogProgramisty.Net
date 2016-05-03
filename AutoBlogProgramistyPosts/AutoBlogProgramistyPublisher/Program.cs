using AutoBlogProgramistyPosts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            Console.WriteLine("Rozpoczęcie publikowania postu na blogu");

            var publisher = new Publisher(new NewsPostCreator(args[0]));

            //publisher.Publish();

            publisher.PublisTwitter(() =>
            {
                Console.WriteLine("Podaj klucz do autoryzacji Twittera");
                return Console.ReadLine();
            });

            Console.WriteLine("Opublikowano post");
        }
    }
}
