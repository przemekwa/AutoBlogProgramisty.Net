﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordPressSharp.Models;

namespace AutoBlogProgramistyPosts
{
    public class NewsFileParser : IPostParser
    {
        public const string HTMLNEWSBODYTEMPLATE = "<ul><li><h3>{0}</h3></li></ul>{1}<blockquote><a href=\"{2}\">{2}</a></blockquote>";

        public FileInfo FileInfo { get; set; }

        public NewsFileParser(string fileName)
        {
            this.FileInfo = new FileInfo(fileName);
        }

        public Post GetPost()
        {
            var result = new Post();

            var news = this.GetNewsFromFile();

            if (news == null)
            {
                return null;
            }

            return result; 
        }

        public string GetHtmlBody(NewsDto news)
        {
            var result = new StringBuilder();

            result.Append(news.Header);

            var moreSign = "<!--more-->";

            foreach (var n in news.LinksList)
            {
                result.AppendFormat(HTMLNEWSBODYTEMPLATE, n.Header, moreSign, n.Url);

                moreSign = string.Empty;
            }

            return result.ToString();
        }

        public NewsDto GetNewsFromFile()
        {
            var newsList = new List<NewsDto>();

            var lines = File.ReadAllLines(this.FileInfo.FullName);

            if (lines.Count() != 7)
            {
                throw new Exception("Plik musi mieć dokładnie 7 lini a ma " + lines.Count());
            }

            return new NewsDto
            {
                Header = lines[0],
                LinksList = new List<LinksDto>
                {
                    new LinksDto { Header = lines[1], Url = lines[2] },
                    new LinksDto { Header = lines[3], Url = lines[4] },
                    new LinksDto { Header = lines[5], Url = lines[6] }
                }
            }; 
        }

        private void WriteNewsToFile(IEnumerable<string> newsList)
        {
            
        }
    }
}
