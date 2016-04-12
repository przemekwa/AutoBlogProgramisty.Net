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
            var fileName = "news.txt";

            var publisher = new Publisher(new NewsFileParser(fileName));

            publisher.Publish();

        }
    }
}
