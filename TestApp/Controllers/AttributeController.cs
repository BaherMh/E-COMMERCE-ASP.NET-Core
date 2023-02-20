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
    public class AttributeController : BaseController
    {
        private readonly IMapper _mapper;
        public AttributeController(IUnitOfWork uow, IMapper mapper) : base(uow)
        {
            _mapper = mapper;
        }

        private List<AppDbContext.Models.Attribute> DoSort(List<AppDbContext.Models.Attribute> cats, string sort_expression)
        {
            if (sort_expression.Contains("_desc"))
            {
                cats.Sort((r1, r2) => r2.Name.CompareTo(r1.Name));
            }
            else
            {
                cats.Sort((r1, r2) => r1.Name.CompareTo(r2.Name));
            }

            return cats;
        }

      

        public IActionResult Index(string sortExpression = "Name", string SearchText = "", int pg = 1, int pageSize = 5)
        {
            String x = SearchText;
            var atts_ = _uow.AttributeRepo.GetAll().ToList();
            if (SearchText != "" && SearchText != null)
            {
                atts_ = _uow.AttributeRepo.GetAll().Where(s => s.Name.Contains(SearchText)).ToList();
            }
            var attributes_ = _mapper.Map<List<AttributeViewModel>>(atts_);
            atts_ = DoSort(atts_, sortExpression);


            SortModel sortModel = new SortModel();
            sortModel.AddColumn("Name");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;

            ViewBag.SearchText = SearchText;

            var pager = new PagerModel(attributes_.Count, pg, pageSize, SearchText);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;


            TempData["CurrentPage"] = pg;
            atts_ = atts_.GetRange((pg - 1) * pageSize, Math.Min(pageSize, atts_.Count() - (pg - 1) * pageSize));

            attributes_ = _mapper.Map<List<AttributeViewModel>>(atts_);
            return View("AttributeReadView", attributes_);

        }

        public IActionResult Edit(int id)
        {
            var attribute = _uow.AttributeRepo.Get(id);
            var attribute_ = _mapper.Map<AttributeViewModel>(attribute);
            return PartialView("AttributeEditView", attribute_);
        }
        public IActionResult SaveEdit(AttributeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var attribute = _mapper.Map<AppDbContext.Models.Attribute>(model);
                _uow.AttributeRepo.Update(attribute);
                _uow.SaveChanges();
                return RedirectToAction("index");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Create()
        {

            return View("AttributeCreateView");
        }
        public IActionResult SaveCreate(AttributeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var attribute = _mapper.Map<AppDbContext.Models.Attribute>(model);
                _uow.AttributeRepo.Add(attribute);
                _uow.SaveChanges();
                return RedirectToAction("index");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Delete(int id)
        {
            var attribute = _uow.AttributeRepo.Get(id);
            var attribute_ = _mapper.Map<AttributeViewModel>(attribute);
            return PartialView("AttributeDeleteView", attribute_);
        }

        public IActionResult ConfirmDelete(AttributeViewModel model)
        {
            try
            {
                var attribute = _mapper.Map<AppDbContext.Models.Attribute>(model);
                _uow.AttributeRepo.Delete(attribute.Id);
                _uow.SaveChanges();
                Notify("Deleted Successfully!");
                return RedirectToAction("index");
            }
            catch
            {
                Notify("Couldn`t delete data!", notificationType: sweetalert.Models.NotificationType.error);
                return RedirectToAction("index");

            }

        }

    }
}
