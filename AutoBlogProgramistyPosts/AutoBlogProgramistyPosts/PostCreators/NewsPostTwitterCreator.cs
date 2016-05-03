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

        public NewsPostTwitterCreator(string fileName) : base(fileName)
        {
           
        }

        public void SendTweet(Func<string,string> verifierMethod)
        {
            TwitterService service = new TwitterService(ConfigurationManager.AppSettings["TwitterKey"], ConfigurationManager.AppSettings["TwitterSecret"]);

            //OAuthRequestToken requestToken = service.GetRequestToken();

            //Uri uri = service.GetAuthorizationUri(requestToken);

            //OAuthAccessToken access = service.GetAccessToken(requestToken, verifierMethod.Invoke(uri.ToString()));

            var access = new OAuthAccessToken
            {
                ScreenName = ConfigurationManager.AppSettings["TwitterScreenName"],
                UserId = int.Parse(ConfigurationManager.AppSettings["TwitterUserId"]),
                Token = ConfigurationManager.AppSettings["TwitterToken"],
                TokenSecret = ConfigurationManager.AppSettings["TwitterTokenSecret"]
            };

            service.AuthenticateWith(access.Token, access.TokenSecret);

            service.SendTweet(new SendTweetOptions { Status = "TestAutoBlogProgramisty" + PostLink });
        }
    }
}
