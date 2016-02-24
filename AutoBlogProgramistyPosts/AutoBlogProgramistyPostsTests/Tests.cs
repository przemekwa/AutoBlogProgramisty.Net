using AutoBlogProgramistyPosts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBlogProgramistyPostsTests
{
    public class Tests
    {
        [OneTimeSetUp]
        public void Init()
        {
         
        }

        [Test]
        public void TestTestAdapterTest()
        {
            Assert.Pass();
        }

        [Test]
        public void PostTest()
        {
            using (AppConfig.Change(@"J:\Dropbox\Dropbox\c#\Projects\AutoBlogProgramisty.Net\AutoBlogProgramistyPosts\AutoBlogProgramistyPosts\bin\Debug\AutoBlogProgramistyPosts.dll.config"))
            {
                var awp = new PostsSender();

                Assert.AreEqual(string.Empty, awp.PostPost(null));

            }
        }

        [Test]
        public void GetPostsTest()
        {
            using (AppConfig.Change(@"J:\Dropbox\Dropbox\c#\Projects\AutoBlogProgramisty.Net\AutoBlogProgramistyPosts\AutoBlogProgramistyPosts\bin\Debug\AutoBlogProgramistyPosts.dll.config"))
            {
                var awp = new PostsSender();

                awp.GetPosts();
              

            }
        }
    }
}
