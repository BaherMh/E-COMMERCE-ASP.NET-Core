using AppDbContext.UOW;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using AppDbContext.UOW;
//using Microsoft.AspNetCore.Mvc;
//using System.Linq;
using TestApp.Models;
//using AutoMapper;
using AppDbContext.Models;

namespace TestApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        protected IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ServiceController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet]
        public void getStatus(int id)
        {
            var state = _uow.OrderRepo.Get(id).State;
            OrdersController ordCon = new OrdersController(_uow, _mapper);
            ordCon.ShowStatus(state);
        }
        //public JsonResult getStatus(int id)
        //{
        //    var state = _uow.OrderRepo.Get(id).State;
        //    OrdersController ordCon = new OrdersController(_uow, _mapper);
        //    ordCon.ShowStatus(state);
        //return Json(new {key=value});
        //}

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
