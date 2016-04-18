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

            this.PostCreator.TermTags = this.TermTags;
        }
        
        public string Publish()
        {
            using (wordPressClient)
            {
                var post = this.PostCreator.GetPost();

                // TODO Lepsza obsługa tagów

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


