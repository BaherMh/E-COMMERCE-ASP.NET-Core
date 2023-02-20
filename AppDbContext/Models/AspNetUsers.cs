using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppDbContext.Models
{
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            AspNetUserClaimsUser = new HashSet<AspNetUserClaims>();
            AspNetUserClaimsUserId1Navigation = new HashSet<AspNetUserClaims>();
            AspNetUserLoginsUser = new HashSet<AspNetUserLogins>();
            AspNetUserLoginsUserId1Navigation = new HashSet<AspNetUserLogins>();
            AspNetUserRolesUser = new HashSet<AspNetUserRoles>();
            AspNetUserRolesUserId1Navigation = new HashSet<AspNetUserRoles>();
            AspNetUserTokensUserId1Navigation = new HashSet<AspNetUserTokens>();
            Comment = new HashSet<Comment>();
            Notification = new HashSet<Notification>();
            Order = new HashSet<Order>();
            Rating = new HashSet<Rating>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool? EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool? PhoneNumberConfirmed { get; set; }
        public bool? TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool? LockoutEnabled { get; set; }
        public int? AccessFailedCount { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public virtual AspNetUserTokens AspNetUserTokensUser { get; set; }
        public virtual ICollection<AspNetUserClaims> AspNetUserClaimsUser { get; set; }
        public virtual ICollection<AspNetUserClaims> AspNetUserClaimsUserId1Navigation { get; set; }
        public virtual ICollection<AspNetUserLogins> AspNetUserLoginsUser { get; set; }
        public virtual ICollection<AspNetUserLogins> AspNetUserLoginsUserId1Navigation { get; set; }
        public virtual ICollection<AspNetUserRoles> AspNetUserRolesUser { get; set; }
        public virtual ICollection<AspNetUserRoles> AspNetUserRolesUserId1Navigation { get; set; }
        public virtual ICollection<AspNetUserTokens> AspNetUserTokensUserId1Navigation { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Notification> Notification { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<Rating> Rating { get; set; }
    }
}
