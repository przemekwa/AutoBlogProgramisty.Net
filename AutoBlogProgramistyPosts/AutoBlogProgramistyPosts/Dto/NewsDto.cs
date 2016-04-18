using System.Collections.Generic;

namespace AutoBlogProgramistyPosts
{
    public class NewsDto
    {
        public string Header { get; set; }

        public List<LinksDto> UrlCollection { get; set; }
    }
}
