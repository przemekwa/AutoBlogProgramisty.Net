using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoBlogProgramistyPosts;
using NUnit.Framework;

namespace AutoBlogProgramistyPostsTests
{
    class PublisherTwitterTest
    {
        [Test]
        public void Test()
        {
            using (AppConfig.Change(@"J:\Dropbox\Dropbox\c#\Projects\AutoBlogProgramisty.Net\AutoBlogProgramistyPosts\AutoBlogProgramistyPublisher\App.config"))
            {
                var test = new Publisher(null);

                test.PublisTwitter();

                Assert.Pass();
            }
        }
        
    }
}
