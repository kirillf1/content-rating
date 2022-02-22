using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Shared.Rating
{
    public class UsersRating
    {
        public long ContentId { get; set; } = default!;
        public List<RatedContent> RatedContent { get; set; } = default!;
    }
    public class RatedContent
    {
        public int UserId { get; set; }
        public double Rating { get; set; }
        public bool CanEstimate { get; set; } = false;
    }
    
}
