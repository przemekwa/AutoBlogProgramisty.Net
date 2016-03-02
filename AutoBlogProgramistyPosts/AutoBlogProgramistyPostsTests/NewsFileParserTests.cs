﻿using AutoBlogProgramistyPosts;
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
        public void Get_News_From_File()
        {
            var fileParser = new NewsFileParser(this.FileName);

            var result = fileParser.GetNewsFromFile();

            Assert.AreEqual(result.Header, this.FileContent[0], "Błędny nagłówek");

            Assert.AreEqual(result.LinksList.Count, 3, "Błędny ilość wiadomosći");

            Assert.AreEqual(result.LinksList[1].Header, this.FileContent[3]);
        }

        [Test]
        public void Get_HTMLBody()
        {
            var fileParser = new NewsFileParser(this.FileName);

            var result = fileParser.GetHtmlBody(fileParser.GetNewsFromFile());

            Assert.AreEqual(result, "Czas na kolejną porcja ciekawych i mocno wyselekcjonowanych newsów z zakresu programowania, komputerów i całej branży deweloperskiej. Zapraszam.<ul><li><h3>Koszty tworzenia komentarzy</h3></li></ul><!--more--><blockquote><a href=\"https://jaxenter.com/costs-and-benefits-of-comments-124166.html\">https://jaxenter.com/costs-and-benefits-of-comments-124166.html</a></blockquote><ul><li><h3>SOLID na potocznych przykładach</h3></li></ul><blockquote><a href=\"http://www.daedtech.com/solid-principles-real-life\">http://www.daedtech.com/solid-principles-real-life</a></blockquote><ul><li><h3>Co by tu się pouczyć nowego w 2016 roku?</h3></li></ul><blockquote><a href=\"http://www.infoworld.com/article/3038679/application-development/the-13-developer-skills-you-need-to-master-now.html\">http://www.infoworld.com/article/3038679/application-development/the-13-developer-skills-you-need-to-master-now.html</a></blockquote>");
        }
    }
}
