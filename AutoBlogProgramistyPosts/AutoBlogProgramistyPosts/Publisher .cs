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

            this.wordPressClient = new WordPressClient();
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

        public void Delete(string id)
        {

        }

        public string Publish()
        {
            using (wordPressClient)
            {
                this.PostParser.TermTags = wordPressClient.GetTerms("post_tag", null);

                return wordPressClient.NewPost(this.PostParser.GetPost());
            }
        }
    }
}
     
    
