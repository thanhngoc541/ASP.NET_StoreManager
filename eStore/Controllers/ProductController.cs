using BusinessObject;
using DataAccess.Repository;
using eStore.Controllers.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace eStore.Controllers
{
    public class ProductController : Controller
    {
        ProductRepository productRepository = new ProductRepository();
        OrderRepository orderRepository = new OrderRepository();
        public List<OrderDetail> cart { get; set; }
    
        private int Exists(List<OrderDetail> cart, int id)
        {
            for (var i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.ProductId == id)
                {
                    return i;
                }
            }
            return -1;
        }
        public double Total { get; set; }

        public ActionResult Buy ()
        {
              orderRepository = new OrderRepository();
                 productRepository = new ProductRepository();
                var newOrder = new Order()
                {
                    MemberId = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "id"),
                    OrderDate = DateTime.Now,
                    RequiredDate = DateTime.Now.AddDays(5),
                    ShippedDate = DateTime.Now.AddDays(2),
                    Freight = 54000,
                };
                orderRepository.Add(newOrder);
            cart = SessionHelper.GetObjectFromJson<List<OrderDetail>>(HttpContext.Session, "cart");
            
            foreach (var item in cart)
                {
                    productRepository.SubstractStock(item.Product.ProductId, item.Quantity);
                    var orderDetail = new OrderDetail()
                    {
                        Discount = 0,
                        OrderId = newOrder.OrderId,
                        ProductId = item.Product.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = productRepository.Get(item.Product.ProductId).UnitPrice
                    };
                    var orderDetailRepository = new OrderDetailRepository();
                    orderDetailRepository.Add(orderDetail);
                }
            cart = new List<OrderDetail>();
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction(nameof(Index));
        }
        public ActionResult AddToCart(int id)
        {
            cart = SessionHelper.GetObjectFromJson<List<OrderDetail>>(HttpContext.Session, "cart");
            var product = productRepository.Get(id);
            if (cart == null)
            {
                cart = new List<OrderDetail>();
                cart.Add(new OrderDetail
                {
                    Product = product,
                    Quantity = 1
                });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                int index = Exists(cart, id);
                if (index == -1)
                {
                    cart.Add(new OrderDetail
                    {
                        Product = product,
                        Quantity = 1,
                     
                    });
                }
                else
                {
                    if (cart[index].Quantity< product.UnitInStock)
                    cart[index].Quantity++;
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
          
            return RedirectToAction(nameof(Index));
        }
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(int id, int quantity)
        {
            cart = SessionHelper.GetObjectFromJson<List<OrderDetail>>(HttpContext.Session, "cart");
            var product = productRepository.Get(id);
            if (cart == null)
            {
                cart = new List<OrderDetail>();
                cart.Add(new OrderDetail
                {
                    Product = product,
                    Quantity = quantity
                });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                int index = Exists(cart, id);
                if (index == -1)
                {
                    cart.Add(new OrderDetail
                    {
                        Product = product,
                        Quantity = quantity,

                    });
                }
                else
                {
                    if (cart[index].Quantity < product.UnitInStock)
                        cart[index].Quantity+= quantity;
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }

            return RedirectToAction(nameof(Index));
        }
        // GET: productController
        public ActionResult Index(string key)
        {
            cart = SessionHelper.GetObjectFromJson<List<OrderDetail>>(HttpContext.Session, "cart");
            if (cart == null) cart = new List<OrderDetail>();
            Total = (double)cart.Sum(i => i.Product.UnitPrice * i.Quantity);
            ViewData["Cart"] = cart.Count.ToString();
            ViewData["Total"] = Total;
            ViewData["key"] = key;
            if (String.IsNullOrEmpty(key))
            return View(productRepository.GetAll());
            return View(productRepository.Search(key.ToString().Trim()));
        }

        // GET: productController/Details/5
        public ActionResult Details(int id)
        {

            var product = productRepository.Get(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // GET: productController/Create
        public ActionResult Create()
        {
            int aID = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "id");
            if (aID == 1)
            return View();
            return RedirectToAction(nameof(Index));
        }

        // POST: productController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    productRepository.Add(product);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(product);
            }
        }

        // GET: productController/Edit/5
        public ActionResult Edit(int id)
        {
            int aID = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "id");
            if (aID == 1 || aID == id)
            {
                var product = productRepository.Get(id);
                if (product == null) return NotFound();
                return View(product);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: productController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            try
            {
                if (id != product.ProductId) return NotFound();
                if (ModelState.IsValid)
                {
                    productRepository.Update(product);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(product);
            }
        }

        // GET: productController/Delete/5
        public ActionResult Delete(int id)
        {
            int aID = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "id");
            if (aID == 1 || aID == id)
            {
                var product = productRepository.Get(id);
                if (product == null) return NotFound();
                return View(product);
            }
            else
                return RedirectToAction(nameof(Index));
        }

        // POST: productController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                productRepository.Remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
