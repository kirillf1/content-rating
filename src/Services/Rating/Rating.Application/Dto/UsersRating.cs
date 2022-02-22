using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rating.Application.Dto
{
    public class UsersRating
    {
        public UsersRating(long contentId, IEnumerable<RatedContent> ratedContent)
        {
            ContentId = contentId;
            RatedContent = new List<RatedContent>();
            RatedContent.AddRange(ratedContent);
        }
        public UsersRating()
        {
            RatedContent = new List<RatedContent>();
        }
        public long ContentId { get; set; }
        public List<RatedContent> RatedContent { get; set; }
    }
    public class RatedContent
    {
        public RatedContent()
        {

        }
        public RatedContent(int userId,double rating)
        {
            UserId = userId;
            Rating = rating;
        }
        public int UserId { get; set; }
        public double Rating { get; set; }
    }
}
