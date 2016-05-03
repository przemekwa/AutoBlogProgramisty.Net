using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBlogProgramistyPosts
{
    public abstract class NewBaseCreator
    {
        public FileInfo FileInfo { get; private set; }

        public NewBaseCreator(string fileName)
        {
            this.FileInfo = new FileInfo(fileName);
        }

        public NewsDto GetNewsFromFile()
        {
            var lines = File.ReadAllLines(this.FileInfo.FullName);

            var result = new NewsDto
            {
                Header = lines[0],
                UrlCollection = new List<LinksDto>()
            };

            for (int i = 1; i < lines.Length; i = i + 2)
            {
                result.UrlCollection.Add(new LinksDto
                {
                    Header = lines[i],
                    Url = lines[i + 1]
                });
            }

            return result;
        }
    }
}
