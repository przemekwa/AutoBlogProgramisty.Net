using WordPressSharp.Models;

namespace AutoBlogProgramistyPosts
{
    public interface IPost
    {
        Term[] TermTags { get; set; }
        Post GetPost();
    }
}