using AppDbContext.UOW;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class ProductUserController : BaseController
    {
        private readonly IMapper _mapper;
        public ProductUserController(IUnitOfWork uow, IMapper mapper) : base(uow)
        {
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var prods_ = _uow.ProductRepo.GetAll().ToList();
           
            var products_ = _mapper.Map<List<ProductViewModel>>(prods_);
           
            return View("Index", products_);
        }
    }
}
