using AppDbContext.Models;
using AppDbContext.UOW;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment webHostEnvironment;
        private List<Product> DoSort(List<Product> cats, string sort_expression)
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
        public ProductController(IUnitOfWork uow, IMapper mapper, IWebHostEnvironment _webHostEnvironment) : base(uow)
        {
            _mapper = mapper;
            webHostEnvironment = _webHostEnvironment;
        }

        // GET: ProductController
        //public ActionResult Index()
        //{
        //    var products = _uow.ProductRepo.GetAll().ToList();
        //    var products_ = _mapper.Map<List<ProductViewModel>>(products);

        //    //var products = _uow.ProductRepo.Get(1);
        //    //var products_ = _mapper.Map<ProductViewModel>(products);

        //    return View("ProductReadView", products_);
        //}

        public IActionResult Index(string sortExpression = "Name", string SearchText = "", int pg = 1, int pageSize = 5)
        {
            var session_id = HttpContext.Session.GetString("UserId");

            if (session_id=="1")
            {
            String x = SearchText;
            var prods_ = _uow.ProductRepo.GetAll().ToList();
            if (SearchText != "" && SearchText != null)
            {
                prods_ = _uow.ProductRepo.GetAll().Where(s => s.Name.Contains(SearchText)).ToList();
            }
            var products_ = _mapper.Map<List<ProductViewModel>>(prods_);
            prods_ = DoSort(prods_, sortExpression);


            SortModel sortModel = new SortModel();
            sortModel.AddColumn("Name");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;

            ViewBag.SearchText = SearchText;

            var pager = new PagerModel(products_.Count, pg, pageSize, SearchText);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;


            TempData["CurrentPage"] = pg;
            prods_ = prods_.GetRange((pg - 1) * pageSize, Math.Min(pageSize, prods_.Count() - (pg - 1) * pageSize));

            products_ = _mapper.Map<List<ProductViewModel>>(prods_);
            return View("ProductReadView", products_);

            }
            else
            {
                return View("Views/Home/Index.cshtml");
            }
           
        }

        // GET: ProductController/Det ails/5
        public ActionResult Details(int id)
        {
            var product = _uow.ProductRepo.Get(id);
            var product_ = _mapper.Map<ProductViewModel>(product);
            var productAttr = _uow.ProductAttrRepo.GetAll().ToList();
            var productAttr_ = _mapper.Map<List<ProductAttrViewModel>>(productAttr);
            product_.ProductAttributes = new List<ProductAttrViewModel>();
            int counter = 0;
            foreach (ProductAttrViewModel pA in productAttr_)
            {
                //pA.ProductId = product_.Id;

                if (pA.ProductId == product_.Id)
                {
                    counter++;
                    product_.ProductAttributes.Add(pA);
                }
            }
            product_.attributesNumber = counter;
            var ProductAttributes = _uow.ProductAttrRepo.GetAll().ToList();
            ProductAttributes = ProductAttributes.FindAll(s => s.ProductId == product_.Id);
            var ProductAttributes_ = _mapper.Map<List<ProductAttrViewModel>>(ProductAttributes);

            var attrs = ProductAttributes_.Select(x => {
                var attr = _uow.AttributeRepo.Get(x.AttrId);
                return _mapper.Map<AttributeViewModel>(attr);
            }).ToList();

            var units = ProductAttributes_.Select(x => {
                var unit = _uow.UnitRepo.Get(x.UnitId);
                return _mapper.Map<UnitViewModel>(unit);
            }).ToList();

            var values = ProductAttributes_.Select(x => {
                return x.Value;
            }).ToList();
            //var prods = ProductAttributes_.Select(x => {
            //    var prod = _uow.ProductRepo.Get(x.ProductId);
            //    return _mapper.Map<ProductViewModel>(prod);
            //}).ToList();



            product_.ProductAttributes = ProductAttributes_;

            var category_ = _uow.CategoryRepo.Get(product_.CategoryId);
            product_.Category = _mapper.Map<CategoryViewModel>(category_);

            var CategoryAttributes = _uow.CategoryAttrRepo.GetAll().ToList();
            CategoryAttributes = CategoryAttributes.FindAll(s => s.CategoryId == product_.CategoryId);
            var CategoryAttributes_ = _mapper.Map<List<CategoryAttrViewModel>>(CategoryAttributes);

            var attrs_cat = CategoryAttributes_.Select(x => {
                var attr = _uow.AttributeRepo.Get(x.AttrId);
                return _mapper.Map<AttributeViewModel>(attr);
            }).ToList();

            var units_cat = CategoryAttributes_.Select(x => {
                var unit = _uow.UnitRepo.Get(x.UnitId);
                return _mapper.Map<UnitViewModel>(unit);
            }).ToList();

            var values_cat = CategoryAttributes_.Select(x => {
                return x.Value;
            }).ToList();
            //var cats_cat = CategoryAttributes_.Select(x => {
            //    var cat = _uow.CategoryRepo.Get(x.CategoryId);
            //    return _mapper.Map<CategoryViewModel>(cat);
            //}).ToList();


            for (int i = 0; i < CategoryAttributes_.Count(); i++)
            {
                if (attrs.Select(e=>e.Name).ToList().Contains(attrs_cat[i].Name))
                {
                    continue;
                }
                else
                {
                    attrs.Add(attrs_cat[i]);
                    units.Add(units_cat[i]);
                    values.Add(values_cat[i]);

                    ProductAttributes_.Add(new ProductAttrViewModel());
                    
                    //attrs.Add(attrs_cat[i]);
                }
            }
            for (int i = 0; i < attrs.Count(); i++)
            {
                ProductAttributes_[i].Attr = attrs[i];
                //ProductAttributes_[i].Product = prods[i];
                ProductAttributes_[i].Unit = units[i];
                ProductAttributes_[i].Value = values[i];
            }
            //pro.CategoryAttributes = CategoryAttributes_;
            product_.ProductAttributes = ProductAttributes_;
            product_.attributesNumber = product_.ProductAttributes.Count();
            return View("ProductDetails", product_);
        }
        // GET: ProductController/Create
        public ActionResult Create()
        {
            //ViewBag.CategoryId
            ViewBag.Categories = _uow.CategoryRepo.GetAll().ToList();

            ViewBag.Products = _uow.ProductRepo.GetAll().ToList();
            ViewBag.Attributes = _uow.AttributeRepo.GetAll().ToList();
            ViewBag.Units = _uow.UnitRepo.GetAll().ToList();
            //ViewBag.Values = _uow.ValueRepo.GetAll().ToList();

            //ProductAttr productAttr = new ProductAttr();
            //ViewBag.ProductAttrbutes = _uow.ProductAttrRepo.Get(product.Id);
            //attributesNumber = 0;
            ProductViewModel model = new ProductViewModel();
            return View("ProductCreateView", model);
        }
        public async Task<ActionResult> SaveCreateAsync(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.filePhoto != null)
                {
                    string folder = "Products/Photos/";
                    folder += Guid.NewGuid().ToString() + model.filePhoto.FileName;
                    //string 
                    string serverFolder = Path.Combine(webHostEnvironment.WebRootPath, folder);
                    await model.filePhoto.CopyToAsync(new FileStream(serverFolder,FileMode.Create));
                    model.ProductPhoto = folder;
                    //mode
                }
                
                var product_ = _mapper.Map<Product>(model);
                _uow.ProductRepo.Add(product_);
                _uow.SaveChanges();

                var productAttrs = _mapper.Map<List<ProductAttr>>(model.ProductAttributes);
                foreach (ProductAttr pA in productAttrs)
                {
                    pA.ProductId = product_.Id;
                    _uow.ProductAttrRepo.Add(pA);
                }
                _uow.SaveChanges();
                Notify("A new product created successfully!");
                return RedirectToAction("index");
            }
            else
            {
                Notify("Couldn`t save data!", notificationType: sweetalert.Models.NotificationType.error);
                return View();
            }
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public IActionResult Edit(int id)
        {
            int counter = 0;

            ViewBag.Categories = _uow.CategoryRepo.GetAll().ToList();
            ViewBag.Products = _uow.ProductRepo.GetAll().ToList();
            ViewBag.Attributes = _uow.AttributeRepo.GetAll().ToList();
            ViewBag.Units = _uow.UnitRepo.GetAll().ToList();
            //ViewBag.Values = _uow.ValueRepo.GetAll().ToList();

            var product = _uow.ProductRepo.Get(id);
            ProductViewModel product_ = _mapper.Map<ProductViewModel>(product);

            //var product_ = _mapper.Map<Product>(model);
            //_uow.ProductRepo.Add(product_);
            //_uow.SaveChanges();

            var productAttr = _uow.ProductAttrRepo.GetAll().ToList();
            var productAttr_ = _mapper.Map<List<ProductAttrViewModel>>(productAttr);

            product_.ProductAttributes = new List<ProductAttrViewModel>();
            //product_.ProductPhoto = "~/" + product.ProductPhoto;
            foreach (ProductAttrViewModel pA in productAttr_)
            {
                //pA.ProductId = product_.Id;

                if (pA.ProductId == product_.Id)
                {
                    counter++;
                    product_.ProductAttributes.Add(pA);
                }
            }
            product_.attributesNumber = counter;
            //ViewBag.Categories = _uow.CategoryRepo.GetAll().ToList();
            if (product_.attributesNumber == 0)
            {
                product_.ProductAttributes.Add(new ProductAttrViewModel());
                product_.attributesNumber++;
            }

            return View("ProductEditView", product_);
        }
        public async Task<IActionResult> SaveEditAsync(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                //    var product_ = _mapper.Map<Product>(model);
                //    _uow.ProductRepo.Update(product_);
                //    _uow.SaveChanges();
                if (model.filePhoto != null)
                {
                    string folder = "Products/Photos/";
                    folder += Guid.NewGuid().ToString() + model.filePhoto.FileName;
                    //string 
                    string serverFolder = Path.Combine(webHostEnvironment.WebRootPath, folder);
                    await model.filePhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    model.ProductPhoto = folder;
                    //mode
                }
                var product_ = _mapper.Map<Product>(model);
                _uow.ProductRepo.Update(product_);
                _uow.SaveChanges();

                var Attrs = _uow.ProductAttrRepo.GetAll();
                foreach (ProductAttr pA in Attrs)
                {
                    if (pA.ProductId == product_.Id)
                    {
                        _uow.ProductAttrRepo.Delete(pA.Id);
                        _uow.SaveChanges();
                    }
                }

                var productAttrs = _mapper.Map<List<ProductAttr>>(model.ProductAttributes);
                //product_.ProductAttributes = new List<ProductAttrViewModel>();

                foreach (ProductAttr pA in productAttrs)
                {
                    pA.ProductId = product_.Id;
                    _uow.ProductAttrRepo.Add(pA);
                }
                _uow.SaveChanges();
                Notify("Product updated successfullly");
                return RedirectToAction("index");
            }
            else
            {
                return View();
            }
        }
        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {

            var product = _uow.ProductRepo.Get(id);
            var product_ = _mapper.Map<ProductViewModel>(product);
            //return View("ProductDeleteView", product_);
            Notify("This will be a Cascade-delete!", notificationType: sweetalert.Models.NotificationType.warning);
            return PartialView("ProductDeleteView", product_);
        }
        public IActionResult ConfirmDelete(ProductViewModel model)
        {
            try
            {
                //var product_ = _mapper.Map<Product>(model);
                var product_ = _uow.ProductRepo.Get(model.Id);

                var Attrs = _uow.ProductAttrRepo.GetAll();
                foreach (ProductAttr pA in Attrs)
                {
                    //pA.ProductId = product_.Id;

                    if (pA.ProductId == product_.Id)
                    {
                        _uow.ProductAttrRepo.Delete(pA.Id);
                        _uow.SaveChanges();
                    }
                }

                _uow.ProductRepo.Delete(model.Id);
                _uow.SaveChanges();
                Notify("Product deleted successfully!");
                return RedirectToAction("index");
            }
            catch
            {
                Notify("Couldn`t delete data!", notificationType: sweetalert.Models.NotificationType.error);
                return RedirectToAction("index");

            }
          
        }
        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
