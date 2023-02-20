using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDbContext.UOW;
//using Microsoft.AspNetCore.Mvc;
//using System.Linq;
using TestApp.Models;
using AutoMapper;
using AppDbContext.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace TestApp.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CategoryController(IUnitOfWork uow, IMapper mapper, IWebHostEnvironment webHostEnvironment) : base(uow)
        {
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        private List<Category> DoSort(List<Category> cats, string sort_expression)
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
            var categs_ = _uow.CategoryRepo.GetAll().ToList();
            if (SearchText != "" && SearchText != null)
            {
                categs_ = _uow.CategoryRepo.GetAll().Where(s => s.Name.Contains(SearchText)).ToList();
            }
            var categories_ = _mapper.Map<List<CategoryViewModel>>(categs_);
            categs_ = DoSort(categs_, sortExpression);


            SortModel sortModel = new SortModel();
            sortModel.AddColumn("Name");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;

            ViewBag.SearchText = SearchText;

            var pager = new PagerModel(categories_.Count, pg, pageSize, SearchText);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;


            TempData["CurrentPage"] = pg;
            categs_ = categs_.GetRange((pg - 1) * pageSize, Math.Min(pageSize, categs_.Count() - (pg - 1) * pageSize));
            
            categories_ = _mapper.Map<List<CategoryViewModel>>(categs_);
            return View("CategoryReadView", categories_);

        }
        public ActionResult Details(int id)
        {
            var category = _uow.CategoryRepo.Get(id);
            var category_ = _mapper.Map<CategoryViewModel>(category);
            var categoryAttr = _uow.CategoryAttrRepo.GetAll().ToList();
            var categoryAttr_ = _mapper.Map<List<CategoryAttrViewModel>>(categoryAttr);
            category_.CategoryAttributes = new List<CategoryAttrViewModel>();
            int counter = 0;
            foreach (CategoryAttrViewModel pA in categoryAttr_)
            {
                //pA.ProductId = product_.Id;

                if (pA.CategoryId == category_.Id)
                {
                    counter++;
                    category_.CategoryAttributes.Add(pA);
                }
            }
            category_.attributesNumber = counter;
            var CategoryAttributes = _uow.CategoryAttrRepo.GetAll().ToList();
            CategoryAttributes = CategoryAttributes.FindAll(s => s.CategoryId == category_.Id);
            var CategoryAttributes_ = _mapper.Map<List<CategoryAttrViewModel>>(CategoryAttributes);

            var attrs = CategoryAttributes_.Select(x=>{
                var attr = _uow.AttributeRepo.Get(x.AttrId);
                return _mapper.Map<AttributeViewModel>(attr);
           }).ToList();

            var units = CategoryAttributes_.Select(x => {
                var unit = _uow.UnitRepo.Get(x.UnitId);
                return _mapper.Map<UnitViewModel>(unit);
            }).ToList();

            var cats = CategoryAttributes_.Select(x => {
                var cat = _uow.CategoryRepo.Get(x.CategoryId);
                return _mapper.Map<CategoryViewModel>(cat);
            }).ToList();


            for(int i=0;i< CategoryAttributes_.Count();i++)
            {
                CategoryAttributes_[i].Attr = attrs[i];
                CategoryAttributes_[i].Category = cats[i];
                CategoryAttributes_[i].Unit = units[i];
            }
            category_.CategoryAttributes = CategoryAttributes_;

            return View("CategoryDetails", category_);
        }
        public IActionResult Edit(int id)
        {

            int counter = 0;
            ViewBag.Categories = _uow.CategoryRepo.GetAll().ToList();
            ViewBag.Products = _uow.ProductRepo.GetAll().ToList();
            ViewBag.Attributes = _uow.AttributeRepo.GetAll().ToList();
            ViewBag.Units = _uow.UnitRepo.GetAll().ToList();
            //ViewBag.Values = _uow.ValueRepo.GetAll().ToList();

            var category = _uow.CategoryRepo.Get(id);
            var category_ = _mapper.Map<CategoryViewModel>(category);

            //var product_ = _mapper.Map<Product>(model);
            //_uow.ProductRepo.Add(product_);
            //_uow.SaveChanges();

            var categoryAttr = _uow.CategoryAttrRepo.GetAll().ToList();
            var categoryAttr_ = _mapper.Map<List<CategoryAttrViewModel>>(categoryAttr);
            category_.CategoryAttributes = new List<CategoryAttrViewModel>();

            foreach (CategoryAttrViewModel pA in categoryAttr_)
            {
                //pA.CategoryId = category_.Id;

                if (pA.CategoryId == category_.Id)
                {
                    counter++;
                    category_.CategoryAttributes.Add(pA);
                }
            }
            category_.attributesNumber = counter;
            //ViewBag.Categories = _uow.CategoryRepo.GetAll().ToList();
            if(category_.attributesNumber == 0)
            {
                category_.CategoryAttributes.Add(new CategoryAttrViewModel());
                category_.attributesNumber++;
            }

            return View("CategoryEditView", category_);
        }
        public async Task<IActionResult> SaveEditAsync(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.filePhoto != null)
                {
                    string folder = "Products/Photos/";
                    folder += Guid.NewGuid().ToString() + model.filePhoto.FileName;
                    //string 
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await model.filePhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    model.PhotoUrl = folder;
                    //mode
                }
                var category_ = _mapper.Map<Category>(model);
                _uow.CategoryRepo.Update(category_);
                _uow.SaveChanges();


                var Attrs = _uow.CategoryAttrRepo.GetAll();
                foreach (CategoryAttr pA in Attrs)
                {
                    if (pA.CategoryId == category_.Id)
                    {
                        _uow.CategoryAttrRepo.Delete(pA.Id);
                        _uow.SaveChanges();
                    }
                }

                var categoryAttrs = _mapper.Map<List<CategoryAttr>>(model.CategoryAttributes);
                //category_.CategoryAttributes = new List<CategoryAttrViewModel>();

                foreach (CategoryAttr pA in categoryAttrs)
                {
                    pA.CategoryId = category_.Id;
                    _uow.CategoryAttrRepo.Add(pA);
                }
                _uow.SaveChanges();
                Notify("Data saved successfully");
                return RedirectToAction("index");
            }
            else
            {
                Notify("Couldn`t save data!",notificationType: sweetalert.Models.NotificationType.error);
                return View();
            }
        }
        public IActionResult Create()
        {
            ViewBag.Categories = _uow.CategoryRepo.GetAll().ToList();

            ViewBag.Products = _uow.ProductRepo.GetAll().ToList();
            ViewBag.Attributes = _uow.AttributeRepo.GetAll().ToList();
            ViewBag.Units = _uow.UnitRepo.GetAll().ToList();
            //ViewBag.Values = _uow.ValueRepo.GetAll().ToList();
            CategoryViewModel category = new CategoryViewModel();
            return View("CategoryCreateView", category);
        }
        public async Task<IActionResult> SaveCreateAsync(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.filePhoto != null)
                {
                    string folder = "Products/Photos/";
                    folder += Guid.NewGuid().ToString() + model.filePhoto.FileName;
                    //string 
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await model.filePhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    model.PhotoUrl = folder;
                    //mode
                }
                var category_ = _mapper.Map<Category>(model);
                _uow.CategoryRepo.Add(category_);
                _uow.SaveChanges();

                var categoryAttrs = _mapper.Map<List<CategoryAttr>>(model.CategoryAttributes);
                foreach (CategoryAttr pA in categoryAttrs)
                {
                    pA.CategoryId = category_.Id;
                    _uow.CategoryAttrRepo.Add(pA);
                }
                _uow.SaveChanges();
                Notify("A new category was created successfully");
                return RedirectToAction("index");
            }
            else
            {
                Notify("Couldn`t save data!", notificationType: sweetalert.Models.NotificationType.error);
                return View();
            }
        }
        public IActionResult Delete(int id)
        {
            var categs_ = _uow.CategoryRepo.Get(id);
            var categories_ = _mapper.Map<CategoryViewModel>(categs_);
            Notify("This will be a 'Cascad-delete'!", notificationType: sweetalert.Models.NotificationType.warning);
            return PartialView("CategoryDeleteView", categories_);
        }

        public IActionResult ConfirmDelete(CategoryViewModel model)
        {
            var category_ = _uow.CategoryRepo.Get(model.Id);

            var products = _uow.ProductRepo.GetAll();
            foreach (Product p in products)
            {
                if (p.CategoryId == model.Id)
                {
                    Notify("Couldn`t delete data!", notificationType: sweetalert.Models.NotificationType.error);

                    return RedirectToAction("index");
                }
            }

            var Attrs = _uow.CategoryAttrRepo.GetAll();
            foreach (CategoryAttr cA in Attrs)
            {
                //pA.ProductId = product_.Id;

                if (cA.CategoryId == category_.Id)
                {
                    _uow.CategoryAttrRepo.Delete(cA.Id);
                    _uow.SaveChanges();
                }
            }

            _uow.CategoryRepo.Delete(model.Id);
            _uow.SaveChanges();
            Notify("Data deleted successfully");
            return RedirectToAction("index");
        }
    }
}
