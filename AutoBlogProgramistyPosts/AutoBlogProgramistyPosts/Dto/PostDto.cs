using System;
using WordPressSharp.Models;

namespace AutoBlogProgramistyPosts.Dto
{
    public class PostDto
    {
        public string Author { get; set; }

        public string PostType { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishDateTime { get; set; }

        public string Status { get; set; }

        public string FeaturedImageId { get; set; }

        //TODO pozybyć się odwołań do wordpress-a
        public Term[] Terms { get; set; }

        public string ShortMsg { get; set; }

        public string Link { get; set; }
    }
}
