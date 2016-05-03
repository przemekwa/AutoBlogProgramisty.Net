using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoBlogProgramistyPosts.Dto;
using WordPressSharp.Models;

namespace AutoBlogProgramistyPosts
{
    public interface IPostEngine
    {
        PostDto PublishPost();
    }
}
