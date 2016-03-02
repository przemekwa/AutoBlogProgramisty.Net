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
        public readonly IPostParser PostParser;

        public Publisher(IPostParser postParser)
        {
            this.PostParser = postParser;
            this.wordPressClient = new WordPressClient();
        }

        public void Get()
        {
            using (wordPressClient)
            {
                var test = wordPressClient.GetPosts(null);

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
                return wordPressClient.NewPost(this.PostParser.GetPost());
            }
        }
    }
}
     
    
