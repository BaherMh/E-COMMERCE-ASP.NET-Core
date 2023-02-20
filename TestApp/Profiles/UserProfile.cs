using AppDbContext.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Models;

namespace TestApp.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<AspNetUsers, UserViewModel>();
            CreateMap<AspNetUsers, UserViewModel>().ReverseMap();
        }
    }
}
