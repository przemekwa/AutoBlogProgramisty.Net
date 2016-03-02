using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBlogProgramistyPosts
{
    public class NewsDto
    {
        public string Header { get; set; }
        public List<LinksDto> LinksList { get; set; }
    }

    public class LinksDto
    {
        public string Header { get; set; }
        public string Url { get; set; }
    }
}
