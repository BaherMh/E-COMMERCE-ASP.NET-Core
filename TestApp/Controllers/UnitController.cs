using AppDbContext.UOW;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Models;
using AppDbContext.Models;

namespace TestApp.Controllers
{
    public class UnitController : BaseController
    {
        private readonly IMapper _mapper;
        public UnitController(IUnitOfWork uow, IMapper mapper) : base(uow)
        {
            _mapper = mapper;
        }

        private List<Unit> DoSort(List<Unit> cats, string sort_expression)
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
            var uns_ = _uow.UnitRepo.GetAll().ToList();
            if (SearchText != "" && SearchText != null)
            {
                uns_ = _uow.UnitRepo.GetAll().Where(s => s.Name.Contains(SearchText)).ToList();
            }
            var units_ = _mapper.Map<List<UnitViewModel>>(uns_);
            uns_ = DoSort(uns_, sortExpression);


            SortModel sortModel = new SortModel();
            sortModel.AddColumn("Name");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;

            ViewBag.SearchText = SearchText;

            var pager = new PagerModel(units_.Count, pg, pageSize, SearchText);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;


            TempData["CurrentPage"] = pg;
            uns_ = uns_.GetRange((pg - 1) * pageSize, Math.Min(pageSize, uns_.Count() - (pg - 1) * pageSize));

            units_ = _mapper.Map<List<UnitViewModel>>(uns_);
            return View("UnitReadView", units_);

        }
        public IActionResult Edit(int id)
        {
            var unit = _uow.UnitRepo.Get(id);
            var unit_ = _mapper.Map<UnitViewModel>(unit);
            return PartialView("UnitEditView", unit_);
        }
        public IActionResult SaveEdit(UnitViewModel model)
        {
            if (ModelState.IsValid)
            {
                var unit = _mapper.Map<Unit>(model);
                _uow.UnitRepo.Update(unit);
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

            return View("UnitCreateView");
        }
        public IActionResult SaveCreate(UnitViewModel model)
        {
            if (ModelState.IsValid)
            {
                var unit = _mapper.Map<AppDbContext.Models.Unit>(model);
                _uow.UnitRepo.Add(unit);
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
            var unit = _uow.UnitRepo.Get(id);
            var unit_ = _mapper.Map<UnitViewModel>(unit);
            //return View("", );
            return PartialView("UnitDeleteView", unit_);
        }

        public IActionResult ConfirmDelete(UnitViewModel model)
        {
            try
            {
                var unit = _mapper.Map<AppDbContext.Models.Unit>(model);
                _uow.UnitRepo.Delete(unit.Id);
                _uow.SaveChanges();
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
