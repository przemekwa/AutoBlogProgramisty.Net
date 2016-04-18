using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WordPressSharp.Models;

namespace AutoBlogProgramistyPosts
{
    public class NewsFileParser : IPost
    {
        public const string HTMLNEWSBODYTEMPLATE = "<ul><li><h3>{0}</h3></li></ul>{1}<blockquote><a href=\"{2}\">{2}</a></blockquote>";

        private const string IMAGEID = "613";

        private const string POSTSTATUS = "publish";
        private const string AUTHOR = "1";
        private const string POSTTYPE = "post";
        private const string TAGSPATTERN = "\\[.*\\]";

        public FileInfo FileInfo { get; set; }

        public Term[] TermTags { get; set; }

        public List<Term> Tags { get; set; } 
               
        public NewsFileParser(string fileName)
        {
            this.FileInfo = new FileInfo(fileName);

            this.Tags = new List<Term> {
                    new Term
                    {
                        Count = 34,
                        Id ="40",
                        Name ="News",
                        Parent = "0",
                        Slug = "news",
                        Taxonomy = "category",
                        TermGroup = "0",
                        TermTaxonomyId = "40"
                    } };
        }

        public Post GetPost()
        {
            var result = new Post
            {
                Author = AUTHOR,
                PostType = POSTTYPE,
                Title = string.Format("News-y programistyczne {0}", DateTime.Now.ToString("dd-MM-yyyy")),
                Content = this.GetHtmlBody(this.GetNewsFromFile()),
                PublishDateTime = DateTime.Now,
                Status = POSTSTATUS,
                FeaturedImageId = IMAGEID,
                Terms = this.Tags.ToArray() 
            };

#if DEBUG
            result.PublishDateTime = DateTime.Now.AddDays(2);
#endif
            return result; 
        }

        public string GetHtmlBody(NewsDto news)
        {
            var result = new StringBuilder();

            result.Append(news.Header);

            var moreSign = "<p><span id=\"more - 1099\"></span></p>";

            foreach (var n in news.LinksList)
            {
                this.AddTags(n.Header);

                result.AppendFormat(HTMLNEWSBODYTEMPLATE, this.RemoveTags(n.Header), string.Empty, n.Url);

                moreSign = string.Empty;
            }

            return result.ToString();
        }

        public string RemoveTags(string header)
        {
            return Regex.Replace(header, TAGSPATTERN, string.Empty);
        }

        public void AddTags(string header)
        {
            var regExp = new Regex(TAGSPATTERN);

            var result = regExp.Match(header);

            foreach (Capture item in result.Captures)
            {
                var stringItem = item.Value.Substring(1, item.Value.Length - 2);

                var existTerm = TermTags.Where(s => s.Name.ToLower() == stringItem.ToLower()).FirstOrDefault();

                if (existTerm == null)
                {
                    existTerm = new Term
                    {
                        Name = stringItem,
                        Taxonomy = "post_tag"
                    };
                }

                this.Tags.Add(existTerm);
            }
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
    }
}
