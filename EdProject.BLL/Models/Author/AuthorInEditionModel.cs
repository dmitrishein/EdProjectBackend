﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.BLL.Models.Author
{
    public class AuthorInEditionModel
    {
        public long EditionId { get; set; }
        public long AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string EditionTitle { get; set; }
    }
}
