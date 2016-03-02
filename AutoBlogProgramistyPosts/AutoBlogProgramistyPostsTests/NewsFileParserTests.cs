using AutoBlogProgramistyPosts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBlogProgramistyPostsTests
{
    public class NewsFileParserTests
    {
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

        [Test]
        public void Get_Post_From_File()
        {
            var fileParser = new NewsFileParser(this.FileName);

            var result = fileParser.GetNewsFromFile();

            Assert.AreEqual(result.Header, this.FileContent[0], "Błędny nagłówek");

            Assert.AreEqual(result.LinksList.Count, 3, "Błędny ilość wiadomosći");

            Assert.AreEqual(result.LinksList[1].Header, this.FileContent[3]);
        }
    }
}
