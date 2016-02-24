using WordPressSharp.Models;

namespace AutoBlogProgramistyPosts
{
    public interface IPostParser
    {
        Post GetPost(string fileName);
    }
}