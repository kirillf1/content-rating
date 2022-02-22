using Rating.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Helpers.ObjectFactories
{
    public static class ContentFactory
    {
        private static Random random = new Random();
        public static Content CreateContent()
        {
            return new Content($"https://{random.Next()}");
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
