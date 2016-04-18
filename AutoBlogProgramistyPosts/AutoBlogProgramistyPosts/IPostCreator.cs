using WordPressSharp.Models;

namespace AutoBlogProgramistyPosts
{
    public interface IPostCreator
    {
        Term[] TermTags { get; set; }

        Post GetPost();
    }
}