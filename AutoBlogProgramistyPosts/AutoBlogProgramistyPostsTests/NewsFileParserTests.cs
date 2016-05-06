using AutoBlogProgramistyPosts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordPressSharp.Models;
using static AutoBlogProgramistyPosts.Constants;

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
                "Koszty tworzenia komentarzy[komentarze]",
                "https://jaxenter.com/costs-and-benefits-of-comments-124166.html",
                "SOLID na potocznych[dwa] przykładach[jeden]",
                "http://www.daedtech.com/solid-principles-real-life",
                "Co by tu się pouczyć[.NET] nowego w 2016 roku?",
                "http://www.infoworld.com/article/3038679/application-development/the-13-developer-skills-you-need-to-master-now.html",
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
        public void Get_News_Post_From_File()
        {
            var fileParser = new WordPresNewsPostCreator(this.FileName);
          
            var result = fileParser.GetNewsFromFile();

            Assert.AreEqual(result.Header, this.FileContent[0], "Błędny nagłówek");

            Assert.AreEqual(result.UrlCollection.Count, 4, "Błędna ilość wiadomosći");

            Assert.AreEqual(result.UrlCollection[1].Header, this.FileContent[3]);
        }

        [Test]
        public void Get_Html_Body()
        {
            var fileParser = new WordPresNewsPostCreator(this.FileName);

            var result = fileParser.GetHtmlBody(fileParser.GetNewsFromFile());

            Assert.AreEqual(result, "Czas na kolejną porcja ciekawych i mocno wyselekcjonowanych newsów z zakresu programowania, komputerów i całej branży deweloperskiej. Zapraszam.<ul><li><h3>Koszty tworzenia komentarzy</h3></li></ul><p><span id=\"more - 1099\"></span></p><blockquote><a href=\"https://jaxenter.com/costs-and-benefits-of-comments-124166.html\">https://jaxenter.com/costs-and-benefits-of-comments-124166.html</a></blockquote><ul><li><h3>SOLID na potocznych przykładach</h3></li></ul><blockquote><a href=\"http://www.daedtech.com/solid-principles-real-life\">http://www.daedtech.com/solid-principles-real-life</a></blockquote><ul><li><h3>Co by tu się pouczyć nowego w 2016 roku?</h3></li></ul><blockquote><a href=\"http://www.infoworld.com/article/3038679/application-development/the-13-developer-skills-you-need-to-master-now.html\">http://www.infoworld.com/article/3038679/application-development/the-13-developer-skills-you-need-to-master-now.html</a></blockquote><ul><li><h3>Co by tu się pouczyć nowego w 2016 roku?</h3></li></ul><blockquote><a href=\"http://www.infoworld.com/article/3038679/application-development/the-13-developer-skills-you-need-to-master-now.html\">http://www.infoworld.com/article/3038679/application-development/the-13-developer-skills-you-need-to-master-now.html</a></blockquote>");
        }

        [Test]
        public void Parse_1_Tag_From_File()
        {
            var fileParser = new WordPresNewsPostCreator(this.FileName);

            fileParser.AddTags(this.FileContent[1]);

            Assert.IsTrue(fileParser.Tags.Any(t=>t.Name.Equals("komentarze") && t.Taxonomy== TAGTAXONOMY ));
        }

        [Test]
        public void Parse_2_Tags_From_File()
        {
            var fileParser = new WordPresNewsPostCreator(this.FileName);
            fileParser.AddTags(this.FileContent[3]);

            Assert.IsTrue(fileParser.Tags.Any(t => t.Name.Equals("dwa") && t.Taxonomy == TAGTAXONOMY));
            Assert.IsTrue(fileParser.Tags.Any(t => t.Name.Equals("jeden") && t.Taxonomy == TAGTAXONOMY));
        }

        [Test]
        public void Parse_1_Tags_With_Dot()
        {
            var fileParser = new WordPresNewsPostCreator(this.FileName);
            fileParser.AddTags(this.FileContent[5]);

            Assert.IsTrue(fileParser.Tags.Any(t => t.Name.Equals(".NET") && t.Taxonomy == TAGTAXONOMY));
      
        }
    }
}
