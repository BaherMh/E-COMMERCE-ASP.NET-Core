﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppDbContext.Models
{
    public partial class AspNetUserTokens
    {
        public int UserId { get; set; }
        public string LoginProvider { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int? UserId1 { get; set; }

        public virtual AspNetUsers User { get; set; }
        public virtual AspNetUsers UserId1Navigation { get; set; }
    }
}
