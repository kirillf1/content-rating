using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rating.Domain
{
    public class UserContentRating
    {
        public UserContentRating()
        {

        }
        public UserContentRating(User user, Content content,double rating)
        {
            User = user;
            Content = content;
            Rating = rating;
        }
        public int UserId { get; set; }
        public User User { get; set; } = default!;
        public long ContentId { get; set; }
        public Content Content { get; set; } = default!;
        public double Rating { get; set; }
    }
}
