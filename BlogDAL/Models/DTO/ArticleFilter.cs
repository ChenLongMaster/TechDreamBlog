﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDAL.Models.DTO
{
    public class ArticleFilter
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
    }
}
