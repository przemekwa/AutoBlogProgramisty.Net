using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordPressSharp;
using WordPressSharp.Models;

namespace AutoBlogProgramistyPosts
{
    public class PostsSender
    {
        public readonly WordPressClient wordPressClient;

        public PostsSender()
        {
            this.wordPressClient = new WordPressClient();
        }

        public void GetPosts()
        {
            using (wordPressClient)
            {
                var test = wordPressClient.GetPosts(null);

                var test2 = wordPressClient.GetMediaItems(null);
            }
        }

        public void DeletePost(string id)
        {



        }
        public string PostPost(PostJSON pos1t)
        {
            using (wordPressClient)
            {
                var post = new Post
                {
                    PostType = "post", // "post" or "page"
                    Title = "News-y programistyczne 24-02-2016",
                    Content = "Test",
                    PublishDateTime = DateTime.Now.AddDays(2),
                    Status = "publish" // "draft" or "publish"

                };

                post.FeaturedImageId = "1006";
                

                    
                return wordPressClient.NewPost(post);
            }
        }
    }
}
     
    
