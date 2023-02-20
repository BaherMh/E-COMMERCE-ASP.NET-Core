using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppDbContext.Models
{
    public partial class AspNetRoles
    {
        public AspNetRoles()
        {
            AspNetRoleClaimsRole = new HashSet<AspNetRoleClaims>();
            AspNetRoleClaimsRoleId1Navigation = new HashSet<AspNetRoleClaims>();
            AspNetUserRolesRole = new HashSet<AspNetUserRoles>();
            AspNetUserRolesRoleId1Navigation = new HashSet<AspNetUserRoles>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }

        public virtual ICollection<AspNetRoleClaims> AspNetRoleClaimsRole { get; set; }
        public virtual ICollection<AspNetRoleClaims> AspNetRoleClaimsRoleId1Navigation { get; set; }
        public virtual ICollection<AspNetUserRoles> AspNetUserRolesRole { get; set; }
        public virtual ICollection<AspNetUserRoles> AspNetUserRolesRoleId1Navigation { get; set; }
    }
}
