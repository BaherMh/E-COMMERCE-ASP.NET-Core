using AppDbContext.Models;
using AppDbContext.UOW;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TestApp.Data;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IMapper _mapper;
        //public static List<ProductViewModel> cart = new List<ProductViewModel>();
        public static ShoppingCart shopping_cart = new ShoppingCart();
        public OrdersController(IUnitOfWork uow, IMapper mapper) : base(uow)
        {
            _mapper = mapper;
        }
        public IActionResult service()
        {
            return View();
        }
        public async Task<IActionResult> getStatus(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44363/api/services/"+id.ToString()))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return View("ShowStatus", apiResponse);
                    //reservationList = JsonConvert.DeserializeObject<List<Reservation>>(apiResponse);
                }
            }
        }
        public IActionResult Index(int cat_id = -1)
        {
            ViewBag.Categories = _uow.CategoryRepo.GetAll().ToList();
            var prods_ = _uow.ProductRepo.GetAll().ToList();

            var products_ = _mapper.Map<List<ProductViewModel>>(prods_);
            if (cat_id != -1)
            {
                products_ = products_.FindAll(s => s.CategoryId == cat_id);
            }
            return View("Index", products_);
        }
        public IActionResult ShowCart()
        {
            return View("ShoppingCart", shopping_cart);

        }
        public IActionResult AddItemToShoppingCart(int id)
        {
            //var item = _uow.ProductRepo.Get(id);
            //var item_mapped = _mapper.Map<ProductViewModel>(item);
            //cart.Add(item_mapped);
            //return View("ShoppingCart", cart);
            var item = _uow.ProductRepo.Get(id);
            var item_mapped = _mapper.Map<ProductViewModel>(item);
            ShoppingCartItem sh_item = new ShoppingCartItem(item_mapped);
            shopping_cart.add_item(sh_item);
            return View("ShoppingCart", shopping_cart);


        }
        public IActionResult RemoveItemFromShoppingCart(int id)
        {
            var item = _uow.ProductRepo.Get(id);
            var item_mapped = _mapper.Map<ProductViewModel>(item);
            ShoppingCartItem sh_item = new ShoppingCartItem(item_mapped);
            shopping_cart.remove_item(sh_item);
            return View("ShoppingCart", shopping_cart);
        }

        public IActionResult SaveOrder()
        {
            var session_id = HttpContext.Session.GetString("UserId");

            Order order__ = new Order();
            order__.IsPaid = 0;
            order__.OrderDate = DateTime.Now;
            //order__.Price = 1;
            order__.State = "On";
            //order__.Id = 1;
            order__.UserId = int.Parse(session_id);
            order__.ShippingId = 1;
            order__.Price = shopping_cart.total;
            _uow.OrderRepo.Add(order__);
            _uow.SaveChanges();
            //var order = _uow.OrderRepo.Get;

            var orders_list = _uow.OrderRepo.GetAll();
            var this_order = new Order();
            foreach(Order ord in orders_list)
            {
                if(ord.UserId==order__.UserId && ord.OrderDate == order__.OrderDate)
                {
                    this_order = ord;
                    break;
                }
            }
           
            var items_ = shopping_cart.items;
            foreach (var item_ in items_)
            {
                var p_o = new ProductOrder();
                p_o.OrderId = this_order.Id;
                p_o.ProductId = item_.item.Id;
                p_o.Quantity = item_.amount;
                p_o.Price = item_.amount * item_.item.Price;
                _uow.ProductOrderRepo.Add(p_o);
                _uow.SaveChanges();
            }
            _uow.SaveChanges();
            Notify("Order added successfully!, your order id is: "+this_order.Id);
            return RedirectToAction("Index");

        }

        public IActionResult ShowStatus(string state)
        {
            return View("ShowStatus", state);
        }
    }
}
