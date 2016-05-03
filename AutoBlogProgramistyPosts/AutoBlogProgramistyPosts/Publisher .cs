using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using TweetSharp;
using WordPressSharp;
using WordPressSharp.Models;
using static AutoBlogProgramistyPosts.Constants;

namespace AutoBlogProgramistyPosts
{
    public class Publisher
    {
        public readonly WordPressClient wordPressClient;

        public IPostCreator PostCreator { get; set; }

        public IPostTwitterCreator PostTwitterCreator { get; set; }

        private int postId;

        private Term[] TermTags;

        public Publisher(IPostCreator postCreator, IPostTwitterCreator postTwitterCreator = null)
        {
            this.PostCreator = postCreator;
            this.PostTwitterCreator = postTwitterCreator;
            this.wordPressClient = new WordPressClient();
        }

        public int Publish()
        {
            this.TermTags = wordPressClient.GetTerms(TAGTAXONOMY, null);

            using (wordPressClient)
            {
                var post = this.PostCreator.GetPost();

                post.Terms = this.UpdateTags(post.Terms).ToArray();

                this.postId = int.Parse(wordPressClient.NewPost(post));

                return postId;
            }
        }

        public void PublisTwitter(Func<string, string> verifierMethod)
        {
            using (wordPressClient)
            {
                var newsPost = wordPressClient.GetPost(this.postId);

                this.PostTwitterCreator.PostLink = newsPost.Link;
            }
              
           this.PostTwitterCreator.SendTweet(verifierMethod);
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


