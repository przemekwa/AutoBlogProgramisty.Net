using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using AutoBlogProgramistyPosts.Dto;
using TweetSharp;
using WordPressSharp;
using WordPressSharp.Models;
using static AutoBlogProgramistyPosts.Constants;

namespace AutoBlogProgramistyPosts
{
    public class Publisher
    {
        private IPostEngine postEngine;

        public Publisher(IPostEngine postEngine)
        {
            this.postEngine = postEngine;
        }

        public PostDto Publish(IPostCreator postCreator)
        {
            return this.postEngine.PublishPost(postCreator);
        }

        public PostDto Publish(PostDto post)
        {
            return this.postEngine.PublishPost(post);
        }        
    }
}


