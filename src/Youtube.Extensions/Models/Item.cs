﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youtube.Extensions.Models
{
    public class Item
    {
        public string? Kind { get; set; }
        public string? Etag { get; set; }
        public string? Id { get; set; }
        public Snippet? Snippet { get; set; }
    }
}
