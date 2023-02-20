using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppDbContext.Models
{
    public partial class AspNetUserRoles
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int Id { get; set; }
        public int? RoleId1 { get; set; }
        public int? UserId1 { get; set; }

        public virtual AspNetRoles Role { get; set; }
        public virtual AspNetRoles RoleId1Navigation { get; set; }
        public virtual AspNetUsers User { get; set; }
        public virtual AspNetUsers UserId1Navigation { get; set; }
    }
}
