using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "User name is required.")]
        [Display(Name = "User name")]

        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }
        [Display(Name = "Phone number")]

        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Maximum 30 characters")]
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        public string PasswordHash { get; set; }

        [Display(Name = "Age")]

        public int age { get; set; }

        [Display(Name = "Gender")]

        public string gender { get; set; }
        public DateTime createdAt { get; set; }

        public UserViewModel()
        {
            Random rd = new Random();
            int rand_num = rd.Next(100, 200);
            Id = rand_num;
            //EmailConfirmed = 1;
            //PhoneNumberConfirmed = 1;
            //TwoFactorEnabled = 1;
            //LockoutEnabled = 1;
            //AccessFailedCount = 1;
            age = 1;
            gender = "Male";
            createdAt = new DateTime();
        }
    }
}
