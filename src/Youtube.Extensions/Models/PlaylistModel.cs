using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youtube.Extensions.Models
{
    public class PlaylistModel
    {
        public string? Kind { get; set; }
        public string? Etag { get; set; }
        public string? NextPageToken { get; set; }
        public List<Item>? Items { get; set; }
        public PageInfo? PageInfo { get; set; }
    }
}
