using AutoBlogProgramistyPosts.Dto;

namespace AutoBlogProgramistyPosts
{
    public interface IPostEngine
    {
        PostDto PublishPost(IPostCreator postCreator);
        void PublishPost(PostDto postDto);
    }
}
