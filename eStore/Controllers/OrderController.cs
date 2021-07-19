using BusinessObject;
using DataAccess.Repository;
using eStore.Controllers.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore.Controllers
{
    public class OrderController : Controller
    {
        // GET: HomeController1
        OrderRepository orderRepository = new OrderRepository();
        public List<OrderDetail> cart { get; set; }
        public ActionResult Index(DateTime start, DateTime end)
        {

            int aID = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "id");
      
            
                var model = orderRepository.Search(start,end.AddDays(1).AddTicks(-1),aID);
            ViewData["start"] = start.ToShortDateString();
            ViewData["end"] = end.ToShortDateString();
            ViewData["Total"] = model.Sum(Order => (Order.OrderDetails is null ? 0 :
                Order.OrderDetails.Sum(item => item.UnitPrice * item.Quantity *
                (decimal)(1 - item.Discount / 100)))).ToString();
            ViewData["Quantity"] = model.Count.ToString();
            return View(model);
          
        }
            public ActionResult Details(int id)
        {

            var product = orderRepository.Get(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // GET: productController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: productController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    orderRepository.Add(order);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(order);
            }
        }

        // GET: productController/Edit/5
       /* public ActionResult Edit(int id)
        {

            var product = orderRepository.Get(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // POST: productController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Order order)
        {
            try
            {
                if (id != order.OrderId) return NotFound();
                if (ModelState.IsValid)
                {
                    orderRepository.(order);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(product);
            }
        }*/

        // GET: productController/Delete/5
        public ActionResult Delete(int id)
        {
            var product = orderRepository.Get(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // POST: productController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                orderRepository.Remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
