using AppDbContext.Models;
using AppDbContext.UOW;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IMapper _mapper;
        public AccountController(IUnitOfWork uow, IMapper mapper) : base(uow)
        {
            _mapper = mapper;
        }

        private List<Order> DoSort(List<Order> ords, string sort_expression)
        {
            if (sort_expression.Contains("_desc"))
            {
                ords.Sort((r1, r2) => DateTime.Compare(r1.OrderDate, r2.OrderDate));
            }
            else
            {
                ords.Sort((r1, r2) => DateTime.Compare(r2.OrderDate, r1.OrderDate));
            }

            return ords;
        }
       
        public IActionResult Login()
        {
            return View("Login");
        }
        //public IActionResult Logout()
        //{
        //    HttpContext.Session.Remove("UserId");
        //    return RedirectToAction("index");
        //}
        //[/*ValidateAntiForgeryToken*/]
        public IActionResult Login_2(UserViewModel user_)
        {
            var userID = 0;
            if (ModelState.IsValid)
            {
                //var ASP_user = _mapper.Map<AspNetUsers>(user_);
                var users_ = _uow.UserRepo.GetAll();
                foreach(var user in users_)
                {
                    if(user.PasswordHash == user_.PasswordHash && user.Email == user_.Email)
                    {
                        Notify("Login successfully!");
                        userID = user.Id;
                        HttpContext.Session.SetString("UserId", userID.ToString());
                        return RedirectToAction("Profile");
                    }
                }

                Notify("Login failed!", notificationType: sweetalert.Models.NotificationType.error);
                return RedirectToAction("Login");
            }
            else
            {
                Notify("Couldn`t Login!", notificationType: sweetalert.Models.NotificationType.error);
                return View("Users");
            }
        }

        public IActionResult Register()
        {
            return View("Register");
        }
        public IActionResult Register_2(UserViewModel user_)
        {
            if (ModelState.IsValid)
            {
                var ASP_user = _mapper.Map<AspNetUsers>(user_);
                _uow.UserRepo.Add(ASP_user);
                _uow.SaveChanges();

                //var userID = user.Id;
                //HttpContext.Session.SetString("UserId", userID.ToString());
                Notify("Login successfully!");
                return RedirectToAction("Login");
            }
            else
            {
                Notify("Couldn`t Regist!", notificationType: sweetalert.Models.NotificationType.error);
                return View("Users");
            }
        }

        public IActionResult Profile()
        {
            var session_id = HttpContext.Session.GetString("UserId");
            var user = _uow.UserRepo.Get(int.Parse(session_id));
            var user_ = _mapper.Map<UserViewModel>(user);

            return View("Profile", user_);
        }

        public IActionResult Update()
        {
            var session_id = HttpContext.Session.GetString("UserId");
            var user = _uow.UserRepo.Get(int.Parse(session_id));
            var user_ = _mapper.Map<UserViewModel>(user);
            return View("UpdateUser", user_);
        }

        public IActionResult Update_2(UserViewModel user_)
        {
            if (ModelState.IsValid)
            {
                var ASP_user = _mapper.Map<AspNetUsers>(user_);
                _uow.UserRepo.Update(ASP_user);
                _uow.SaveChanges();

                //var userID = user.Id;
                //HttpContext.Session.SetString("UserId", userID.ToString());
                Notify("Updated successfully!");
                return RedirectToAction("Profile");
            }
            else
            {
                Notify("Couldn`t Update!", notificationType: sweetalert.Models.NotificationType.error);
                return RedirectToAction("Profile");
            }
        }
        public IActionResult Index(string sortExpression = "OrderDate", string SearchText = "", int pg = 1, int pageSize = 5)
        {
            String x = SearchText;
            var ords_ = _uow.OrderRepo.GetAll().ToList();
            if (SearchText != "" && SearchText != null)
            {
                ords_ = _uow.OrderRepo.GetAll().Where(s => s.Id == int.Parse(SearchText)).ToList();
            }

            //var orders_ = _mapper.Map<List<OrderViewModel>>(ords_);
            ords_ = DoSort(ords_, sortExpression);
            
            var session_id = HttpContext.Session.GetString("UserId");

            ords_ = ords_.FindAll(e => e.UserId == int.Parse(session_id)).ToList();

            SortModel sortModel = new SortModel();
            sortModel.AddColumn("OrderDate");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;

            ViewBag.SearchText = SearchText;

            var pager = new PagerModel(ords_.Count, pg, pageSize, SearchText);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;


            TempData["CurrentPage"] = pg;
            ords_ = ords_.GetRange((pg - 1) * pageSize, Math.Min(pageSize, ords_.Count() - (pg - 1) * pageSize));

            var orders_ = _mapper.Map<List<OrderViewModel>>(ords_);

            return View("OrdersReadView", orders_);
        }
    }
}
