﻿using AutoBlogProgramistyPosts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using AutoBlogProgramistyPosts.PostCreators;

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

            var publisher = new Publisher(new NewsPostCreator(args[0]), new NewsPostTwitterCreator(args[0], (uri) =>
            {
                Process.Start(uri);

                Console.WriteLine("Podaj klucz do autoryzacji Twittera");

                return Console.ReadLine();
            }));

            publisher.PublisTwitter();


            // publisher.Publish();

        

            Console.WriteLine("Opublikowano post");
        }
    }
}
