using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBlogProgramistyPosts
{
    public class PostJSON
    {
        public string HtmlMsg { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public IEnumerable<string> Category { get; set; }

        public int ImgID { get; set; }
    }
}
