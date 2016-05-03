using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using TweetSharp;
using WordPressSharp;
using WordPressSharp.Models;
using static AutoBlogProgramistyPosts.Constants;

namespace AutoBlogProgramistyPosts
{
    public class Publisher
    {
        public readonly WordPressClient wordPressClient;

        public readonly IPostCreator PostCreator;

        private Term[] TermTags;

        public Publisher(IPostCreator postCreator)
        {
            this.wordPressClient = new WordPressClient();

            this.PostCreator = postCreator;

            this.TermTags = wordPressClient.GetTerms(TAGTAXONOMY, null);
        }

        public string Publish()
        {
            using (wordPressClient)
            {
                var post = this.PostCreator.GetPost();

                post.Terms = this.UpdateTags(post.Terms).ToArray();

                return wordPressClient.NewPost(post);
            }
        }

        public void PublisTwitter(Func<string> verifierMethod)
        {
            TwitterService service = new TwitterService(ConfigurationManager.AppSettings["TwitterKey"], ConfigurationManager.AppSettings["TwitterSecret"]);
            OAuthRequestToken requestToken = service.GetRequestToken();
            Uri uri = service.GetAuthorizationUri(requestToken);
            Process.Start(uri.ToString());
            OAuthAccessToken access = service.GetAccessToken(requestToken, verifierMethod.Invoke());
            service.AuthenticateWith(access.Token, access.TokenSecret);
            var ts = service.SendTweet(new SendTweetOptions { Status = "TestAutoBlogProgramisty" });
        }

        private IEnumerable<Term> UpdateTags(Term[] postTags)
        {
            var tempTermList = new List<Term>(postTags.Where(t => t.Taxonomy != TAGTAXONOMY));
            
            foreach (var item in postTags.Where(i=>i.Taxonomy == TAGTAXONOMY))
            {
                var element = this.TermTags.SingleOrDefault(t => t.Name.ToLower() == item.Name.ToLower());

                if (element == null)
                {
                    var id = wordPressClient.NewTerm(item);
                    item.Id = id;
                    item.TermTaxonomyId = id;
                    element = item;
                }

                tempTermList.Add(element);
            }

            return tempTermList;
        }
    }
}


