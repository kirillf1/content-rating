using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rating.Application.Dto
{
    public class ChangedContentRating
    {
        public ChangedContentRating(int userId, long contentId,double rating)
        {
            UserId = userId;
            ContentId = contentId;
            Rating = rating;
        }
        public ChangedContentRating()
        {

        }
        public int UserId { get; set; }
        public long ContentId { get; set; }
        public double Rating { get; set; }
    }
}
