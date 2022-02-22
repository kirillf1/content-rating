using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Shared.Rating
{
    public class ContentWithRating
    {
        public int UserId { get; set; }
        public long ContentId { get; set; }
        public double Rating { get; set; }
    }
}
