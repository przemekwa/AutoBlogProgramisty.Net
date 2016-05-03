using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBlogProgramistyPosts
{
    public interface IPostTwitterCreator
    {
        string PostLink { get; set; }
        void SendTweet(Func<string,string> verifierMethod);
    }
}
