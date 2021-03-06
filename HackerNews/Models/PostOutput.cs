﻿using System;
using System.Collections.Generic;

namespace HackerNews.Models
{
    public class PostOutput
    {
        public string title { get; set; }
        public string uri { get; set; }
        public string author { get; set; }
        public int points { get; set; }
        public int comments { get; set; }
        public int rank { get; set; }
    }
}
