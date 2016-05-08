using AutoBlogProgramistyPosts.Dto;

namespace AutoBlogProgramistyPosts.PostCreators
{
    public class TwitterNewsPostCreator : NewsBaseCreator, IPostCreator
    {
        public TwitterNewsPostCreator(string fileName) : base(fileName)
        {

        }

        public PostDto GetPost()
        {
            var news = base.GetNewsFromFile();

            return new PostDto
            {
                ShortMsg = news.Header.Substring(0, news.Header.IndexOf('.'))
            };
        }

        private bool CheckLenght(string msg) => msg.Length < 140;
    }
}
