using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AutoBlogProgramistyPosts.Dto;
using WordPressSharp.Models;
using static AutoBlogProgramistyPosts.Constants;

namespace AutoBlogProgramistyPosts
{
    public class NewsPostCreator : NewsBaseCreator, IPostCreator
    {
        public const string HTMLNEWSBODYTEMPLATE = "<ul><li><h3>{0}</h3></li></ul>{1}<blockquote><a href=\"{2}\">{2}</a></blockquote>";
        public const string IMAGEID = "613";
        public const string POSTSTATUS = "publish";
        public const string AUTHOR = "1";
        public const string POSTTYPE = "post";
        public const string TAGSPATTERN = "\\[[^\\[\\]]+\\]";

        public List<Term> Tags { get; set; } 
               
        public NewsPostCreator(string fileName) :base(fileName)
        { 
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

        public PostDto GetPost()
        {
            var result = new PostDto
            {
                Author = AUTHOR,
                PostType = POSTTYPE,
                Title = string.Format("News-y programistyczne {0}", DateTime.Now.ToString("dd-MM-yyyy")),
                Content = this.GetHtmlBody(this.GetNewsFromFile()),
#if DEBUG
                PublishDateTime = DateTime.Now.AddDays(2),
#else
                 PublishDateTime = DateTime.Now,
#endif
                Status = POSTSTATUS,
                FeaturedImageId = IMAGEID,
                Terms = this.Tags.ToArray() 
            };

            return result; 
        }

        public string GetHtmlBody(NewsDto news)
        {
            var result = new StringBuilder();

            result.Append(news.Header);

            // TODO Znaleść sposób na wstawianie znacznikia <!-- more -->
                
            var moreSign = "<p><span id=\"more - 1099\"></span></p>";

            foreach (var n in news.UrlCollection)
            {
                this.AddTags(n.Header);

                result.AppendFormat(HTMLNEWSBODYTEMPLATE, this.RemoveTags(n.Header), moreSign, n.Url);

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

            var result = regExp.Matches(header);

            foreach (Match item in result)
            {
                this.Tags.Add(new Term
                {
                    Name = item.Value.Substring(1, item.Value.Length - 2),
                    Taxonomy = TAGTAXONOMY,
                });
            }
        }

    }
}
