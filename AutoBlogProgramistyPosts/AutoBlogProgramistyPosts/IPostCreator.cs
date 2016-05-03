using AutoBlogProgramistyPosts.Dto;
using WordPressSharp.Models;

namespace AutoBlogProgramistyPosts
{
    public interface IPostCreator
    {
        PostDto GetPost();
    }
}