using AppDbContext.UOW;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using TestApp.Models;
using TestApp.Services;

namespace TestApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IMapper _mapper;
        public HomeController(IUnitOfWork uow, IMapper mapper) : base(uow)
        {
            _mapper = mapper;
        }

        private readonly ILogger<HomeController> _logger;
        private ISingletonRnd singletonService;
        private ITransientRnd transientService;
        private IScopedRnd scopedService;

        //public HomeController(ILogger<HomeController> logger,
        //      ISingletonRnd _SingletonService,
        //    ITransientRnd _transientService,
        //    IScopedRnd _scopedService)
        //{
        //    _logger = logger;
        //    singletonService = _SingletonService;
        //    transientService = _transientService;
        //    scopedService = _scopedService;
        //}
        public IActionResult Index()
        {
            //var session_id = HttpContext.Session.GetString("UserId");
            //if (session_id != null)
            //{
            //    var user = _uow.UserRepo.Get(int.Parse(session_id));
            //    var user_ = _mapper.Map<UserViewModel>(user);
            //    return View(user_);

            //}
            //else
            //{
            //    UserViewModel user_ = new UserViewModel();
            //    user_.Email = "no_email";
            //    return View(user_);
            //}
            //var httpContext = HttpContext;
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            return RedirectToAction("index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //public IActionResult Singleton()
        //{
        //    ViewBag.ServiceType = "Singleton";
        //    return View("ServicesView", singletonService.GetRandom());
        //}

        //public IActionResult Transient()
        //{
        //    ViewBag.ServiceType = "Transient";
        //    return View("ServicesView", transientService.GetRandom());
        //}

        //public IActionResult Scoped()
        //{
        //    ViewBag.ServiceType = "Scoped";
        //    return View("ServicesView", scopedService.GetRandom());
        //}

    }
}
