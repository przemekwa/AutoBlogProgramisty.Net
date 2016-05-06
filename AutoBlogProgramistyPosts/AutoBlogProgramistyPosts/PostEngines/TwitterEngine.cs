using System;
using System.Configuration;
using AutoBlogProgramistyPosts.Dto;
using TweetSharp;

namespace AutoBlogProgramistyPosts.PostCreators
{
    public class TwitterEngine : IPostEngine
    {
        private TwitterService twitterService;

        private Func<string, string> verifierMethod;

        public TwitterEngine(Func<string, string> verifierMethod)
        {
            this.verifierMethod = verifierMethod;
            this.twitterService = new TwitterService(
                ConfigurationManager.AppSettings["TwitterKey"], 
                ConfigurationManager.AppSettings["TwitterSecret"]);
        }

        public void SendTweet(string msg)
        {
            OAuthAccessToken access = GetOAuthAccessToken();

            this.twitterService.AuthenticateWith(access.Token, access.TokenSecret);

            this.twitterService.SendTweet(new SendTweetOptions { Status = msg });
        }

        private OAuthAccessToken GetOAuthAccessToken()
        {
            if (ConfigurationManager.AppSettings["TwitterScreenName"] == null ||
                    ConfigurationManager.AppSettings["TwitterUserId"] == null || 
                        ConfigurationManager.AppSettings["TwitterToken"] == null || 
                            ConfigurationManager.AppSettings["TwitterTokenSecret"] == null)
            {
                OAuthRequestToken requestToken = this.twitterService.GetRequestToken();

                Uri uri = this.twitterService.GetAuthorizationUri(requestToken);

                var access = this.twitterService.GetAccessToken(requestToken, verifierMethod.Invoke(uri.ToString()));

                ConfigurationManager.AppSettings.Add("TwitterScreenName", access.ScreenName);
                ConfigurationManager.AppSettings.Add("TwitterUserId", access.UserId.ToString());
                ConfigurationManager.AppSettings.Add("TwitterToken", access.Token);
                ConfigurationManager.AppSettings.Add("TwitterTokenSecret", access.TokenSecret);

                return access;
            }

            return new OAuthAccessToken
            {
                ScreenName = ConfigurationManager.AppSettings["TwitterScreenName"],
                UserId = int.Parse(ConfigurationManager.AppSettings["TwitterUserId"]),
                Token = ConfigurationManager.AppSettings["TwitterToken"],
                TokenSecret = ConfigurationManager.AppSettings["TwitterTokenSecret"]
            };
        }
               
       
        public PostDto PublishPost(IPostCreator postCreator)
        {
            var post = postCreator.GetPost();

            this.SendTweet(post.Link);

            return post;
        }

        public PostDto PublishPost(PostDto postDto)
        {
            this.SendTweet($"[BLOG] {postDto.ShortMsg} {postDto.Link}" );

            return postDto;
        }
    }
}
