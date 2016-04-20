using System.Collections.Generic;
using System.Linq;
using WordPressSharp;
using WordPressSharp.Models;

namespace AutoBlogProgramistyPosts
{
    public class Publisher
    {
        public readonly WordPressClient wordPressClient;

        public readonly IPostCreator PostCreator;

        private Term[] TermTags;

        public Publisher(IPostCreator postCreator)
        {
            this.PostCreator = postCreator;

            wordPressClient = new WordPressClient();

            this.TermTags = wordPressClient.GetTerms("post_tag", null);
        }

        public string Publish()
        {
            using (wordPressClient)
            {
                var post = this.PostCreator.GetPost();

                post.Terms = this.UpdateTags(post.Terms).ToArray();

                return wordPressClient.NewPost(post);
            }
        }

        private IEnumerable<Term> UpdateTags(Term[] postTags)
        {
            var tempTermList = new List<Term>(postTags.Where(t => t.Taxonomy != "post_tag"));

            foreach (var item in postTags.Where(i=>i.Taxonomy == "post_tag"))
            {
                var element = this.TermTags.SingleOrDefault(t => t.Name.ToLower() == item.Name.ToLower());

                tempTermList.Add(element != null ? element : item);
            }

            return tempTermList;
        }
    }
}


