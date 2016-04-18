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

        public readonly IPostCreator PostParser;

        private Term[] TermTags;

        public Publisher(IPostCreator postParser)
        {
            this.PostParser = postParser;

            wordPressClient = new WordPressClient();

            this.TermTags = wordPressClient.GetTerms("post_tag", null);

            this.PostParser.TermTags = this.TermTags;
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
                var post = this.PostParser.GetPost();

                var listTagsToAdd = post.Terms.Except(this.TermTags.Where(c=>c.Taxonomy == "post_tag"));

                foreach (var item in listTagsToAdd.Where(c=>c.Taxonomy == "post_tag"))
                {
                    this.wordPressClient.NewTerm(item);
                    post.Terms.ToList().Remove(item);
                }

                return wordPressClient.NewPost(post);
            }
        }
    }
}


