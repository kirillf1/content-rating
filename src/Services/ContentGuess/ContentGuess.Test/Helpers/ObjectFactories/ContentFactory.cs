using ContentGuess.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentGuess.Test.Helpers.ObjectFactories
{
    public static class ContentFactory
    {
        private static Random random = new Random();
        public static Content CreateContent()
        {
            var contentType = new ContentType(DateTime.Now.Ticks.ToString());
            return new Content(DateTime.Now.Ticks.ToString(),contentType,new ContentInfo(DateTime.Now.Ticks.ToString()),Enumerable.Empty<Tag>());
        }
        public static List<Content> CreateContent(int count)
        {
            var content = new List<Content>(count);
            for (int i = 0; i < count; i++)
            {
                content.Add(CreateContent());
            }
            return content;
        }
    }
}
