using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace AutoBlogProgramistyPosts.PostCreators
{
    public class NewsPostTwitterCreator : NewBaseCreator, IPostTwitterCreator
    {
        public string PostLink { get; set; }

        private TwitterService twitterService;

        private Func<string, string> verifierMethod;

        public NewsPostTwitterCreator(string fileName, Func<string, string> verifierMethod) : base(fileName)
        {
           this.verifierMethod = verifierMethod;
           this.twitterService = new TwitterService(ConfigurationManager.AppSettings["TwitterKey"], ConfigurationManager.AppSettings["TwitterSecret"]);
        }

        public void SendTweet()
        {
            OAuthAccessToken access = GetOAuthAccessToken();

            this.twitterService.AuthenticateWith(access.Token, access.TokenSecret);

            this.twitterService.SendTweet(new SendTweetOptions { Status = "TestAutoBlogProgramisty" + PostLink });
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

                OAuthAccessToken access = this.twitterService.GetAccessToken(requestToken, verifierMethod.Invoke(uri.ToString()));

                // TODO Zapisywanie do app settings 

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
    }
}
