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
                

               

                Assert.Pass();
            }
        }
        
    }
}
