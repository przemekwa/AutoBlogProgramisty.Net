using AutoBlogProgramistyPosts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBlogProgramistyPostsTests
{
    public class PublisherTests
    {
        //public void Get()
        //{
        //    using (wordPressClient)
        //    {
        //        var test = wordPressClient.GetPosts(null);

        //        var termlist = wordPressClient.GetTerms("post_tag", null);

        //        var test2 = wordPressClient.GetMediaItems(null);

        //        var id = test2.Where(g => g.Link.Contains("main_news_art_2.png")).First();
        //    }
        //}

        public string FileName { get; set; }

        public string[] FileContent { get; set; }

        [OneTimeSetUp]
        public void Init()
        {
            this.FileName = "news.txt";

            this.FileContent = new[] {
                "Czas na kolejną porcja ciekawych i mocno wyselekcjonowanych newsów z zakresu programowania, komputerów i całej branży deweloperskiej. Zapraszam.",
                "Koszty tworzenia komentarzy",
                "https://jaxenter.com/costs-and-benefits-of-comments-124166.html",
                "SOLID na potocznych przykładach",
                "http://www.daedtech.com/solid-principles-real-life",
                "Co by tu się pouczyć nowego w 2016 roku?",
                "http://www.infoworld.com/article/3038679/application-development/the-13-developer-skills-you-need-to-master-now.html"
            };

            File.WriteAllLines(this.FileName, this.FileContent);
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            File.Delete(this.FileName);
        }

        //[Test]
        public void Publish_News_Post_From_File_Test()
        {
            using (AppConfig.Change(@"J:\Dropbox\Dropbox\c#\Projects\AutoBlogProgramisty.Net\AutoBlogProgramistyPosts\AutoBlogProgramistyPosts\bin\Debug\AutoBlogProgramistyPosts.dll.config"))
            {
                var publisher = new Publisher(new NewsPostCreator(this.FileName));

                Assert.AreNotEqual(string.Empty, publisher.Publish());
            }
        }

        //[Test]
        public void Get_Posts_Test()
        {
            using (AppConfig.Change(@"J:\Dropbox\Dropbox\c#\Projects\AutoBlogProgramisty.Net\AutoBlogProgramistyPosts\AutoBlogProgramistyPosts\bin\Debug\AutoBlogProgramistyPosts.dll.config"))
            {
                var awp = new Publisher(new NewsPostCreator("a"));
                
            }
        }
    }
}
