using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using AutoBlogProgramistyPosts.Dto;
using AutoBlogProgramistyPosts.PostCreators;
using AutoBlogProgramistyPosts.PostEngines;
using TweetSharp;
using WordPressSharp;
using WordPressSharp.Models;
using static AutoBlogProgramistyPosts.Constants;

namespace AutoBlogProgramistyPosts
{
    public class NewsPublisher
    {
        public IPostEngine WordPresspostEngine;

        public IPostEngine TwitterpostEngine;

        public NewsPublisher(Func<string, string> twwiterTokeAcesss)
        {
            this.WordPresspostEngine = new WordPressEngine();
            this.TwitterpostEngine = new TwitterEngine(twwiterTokeAcesss);
        }

        public void PublishNews(string fileName)
        {
            var post = this.WordPresspostEngine.PublishPost(new WordPressNewsPostCreator(fileName));

            this.TwitterpostEngine.PublishPost(post);
        }

    }
}


