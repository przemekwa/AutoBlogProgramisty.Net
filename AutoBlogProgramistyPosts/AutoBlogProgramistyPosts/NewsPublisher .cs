using System;
using AutoBlogProgramistyPosts.PostEngines;

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


