﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Term[] Terms { get; set; }

        public string Link { get; set; }
    }
}
