using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeaFood.Models;

namespace SeaFood.Controllers
{
    public class OrderDetailController : Controller
    {
        DBSeaFoodEntities database = new DBSeaFoodEntities();
        // GET: OrderDetail
        public ActionResult GroupByTop5()
        {
            List<OrderDetail> orderD = database.OrderDetails.ToList();
            List<Product> proList = database.Products.ToList();
            var query = from od in orderD
                        join p in proList on od.IDProduct equals p.ProductID into tbl
                        group od by new
                        {
                            idPro = od.IDProduct,
                            namePro = od.Product.ProductName,
                            imagePro = od.Product.ImageProduct1,
                            price = od.Product.Price
                        } into gr
                        orderby gr.Sum(s => s.Quantity) descending
                        select new ViewModel
                        {
                            ProductID = gr.Key.idPro,
                            ProductName = gr.Key.namePro,
                            ImageProduct1 = gr.Key.imagePro,
                            Price = (decimal)gr.Key.price,
                            Sum_Quantity=gr.Sum(s=>s.Quantity)

                        };
            return View(query.Take(5).ToList());
        }
    }
}