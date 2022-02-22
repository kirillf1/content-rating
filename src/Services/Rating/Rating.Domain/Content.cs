using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Rating.Domain
{
    public class Content
    {
        public Content(string url)
        {
            Url = url;
            RatedByUsers = new List<UserContentRating>();
        }
        public string? Name { get; set; }
        public long Id { get; set; }
        public string Url { get; set; }
        public double AverageRating { 
            get 
            {   if (RatedByUsers.Count == 0)
                    return 0;
               return RatedByUsers.Where(c => c.ContentId == Id).Average(c => c.Rating); 
            } 
        }
        [JsonIgnore]
        public List<UserContentRating> RatedByUsers { get; set; }
        
    }
}
