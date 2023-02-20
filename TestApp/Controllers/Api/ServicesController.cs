using AppDbContext.UOW;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        protected IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ServicesController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet("{id}")]
        public ActionResult <IEnumerable<string>> Get(int id)
        {
            var state = _uow.OrderRepo.Get(id).State;
            return new string[] { state };
        }
    }
}
