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
        public PostDto Publish(IPostEngine postEngine)
        {
           return postEngine.PublishPost();
        }
       


        //public void PublishPostOnTwitter()
        //{
        //    using (wordPressClient)
        //    {
        //       var newsPost = wordPressClient.GetPost(this.postId);
                 
        //       this.PostTwitterCreator.PostLink = newsPost.Link;
        //    }
              
        //   this.PostTwitterCreator.SendTweet();
        //}

      
    }
}


