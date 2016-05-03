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
    class NewsPostTwitterCreator : NewBaseCreator, IPostTwitterCreator
    {
        public string PostLink { get; set; }

        public NewsPostTwitterCreator(string fileName) : base(fileName)
        {
           
        }

        public void SendTweet(Func<string> verifierMethod)
        {
            TwitterService service = new TwitterService(ConfigurationManager.AppSettings["TwitterKey"], ConfigurationManager.AppSettings["TwitterSecret"]);

            OAuthRequestToken requestToken = service.GetRequestToken();
            Uri uri = service.GetAuthorizationUri(requestToken);
            Process.Start(uri.ToString());
            OAuthAccessToken access = service.GetAccessToken(requestToken, verifierMethod.Invoke());
            service.AuthenticateWith(access.Token, access.TokenSecret);
            var ts = service.SendTweet(new SendTweetOptions { Status = "TestAutoBlogProgramisty" + PostLink });
        }
    }
}
