using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoBlogProgramistyPosts.Dto;
using WordPressSharp;
using WordPressSharp.Models;

namespace AutoBlogProgramistyPosts.PostEngines
{
    public class WordPressEngine : IPostEngine
    {
        public readonly WordPressClient wordPressClient;

        private Term[] TermTags;

        public IPostCreator PostCreator { get; set; }

        public const string TAGTAXONOMY = "post_tag";

        private PostDto post;

        public WordPressEngine()
        {
            this.wordPressClient = new WordPressClient();
        }

        private IEnumerable<Term> UpdateTags(Term[] postTags)
        {
            var tempTermList = new List<Term>(postTags.Where(t => t.Taxonomy != TAGTAXONOMY));

            foreach (var item in postTags.Where(i => i.Taxonomy == TAGTAXONOMY))
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

        public PostDto PublishPost()
        {
            this.TermTags = wordPressClient.GetTerms(TAGTAXONOMY, null);

            post = this.PostCreator.GetPost();

            var wordPressPost = new Post
            {
                Author = post.Author,
                PostType = post.PostType,
                Title = post.Title,
                Content = post.Content,
                PublishDateTime = post.PublishDateTime,
                Status = post.Status,
                FeaturedImageId = post.FeaturedImageId,
                Terms = post.Terms
            };

            using (wordPressClient)
            {
                wordPressPost.Terms = this.UpdateTags(wordPressPost.Terms).ToArray();

                var postId = int.Parse(wordPressClient.NewPost(wordPressPost));

                post.Link = wordPressClient.GetPost(postId).Link;
            }

            return post;
        }

        public PostDto PublishPost(IPostCreator postCreator)
        {
            this.PostCreator = postCreator;
            return this.PublishPost();
        }

        public PostDto PublishPost(PostDto postDto)
        {
            this.post = postDto;
            this.PublishPost();

            return this.post;
        }
    }
}
