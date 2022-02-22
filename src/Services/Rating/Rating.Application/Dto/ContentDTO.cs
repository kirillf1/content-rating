﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rating.Application.Dto
{
    public class ContentDTO
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string Url { get; set; } = default!;
    }
}
