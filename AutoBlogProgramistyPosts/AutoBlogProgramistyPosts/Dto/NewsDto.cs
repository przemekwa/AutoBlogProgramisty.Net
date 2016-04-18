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

        public List<LinksDto> UrlCollection { get; set; }
    }
}
