using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordPressSharp;
using WordPressSharp.Models;

namespace AutoBlogProgramistyPosts
{
    public class Publisher
    {
        public readonly WordPressClient wordPressClient;

        public readonly IPost PostParser;

        public Publisher(IPost postParser)
        {
            this.PostParser = postParser;

            wordPressClient = new WordPressClient();

            this.PostParser.TermTags = wordPressClient.GetTerms("post_tag", null);
        }
        
        public void Get()
        {
            using (wordPressClient)
            {
                var test = wordPressClient.GetPosts(null);

                var termlist = wordPressClient.GetTerms("post_tag", null);

                var test2 = wordPressClient.GetMediaItems(null);

                var id = test2.Where(g => g.Link.Contains("main_news_art_2.png")).First();
            }
        }

        public string Publish()
        {
            using (wordPressClient)
            {
                return wordPressClient.NewPost(this.PostParser.GetPost());
            }
        }
    }
}


