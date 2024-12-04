using SeaFood.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using PagedList;
using PagedList.Mvc;

namespace SeaFood.Controllers
{
    public class ProductController : Controller
    {
        DBSeaFoodEntities database = new DBSeaFoodEntities();
        // GET: Product
        public ActionResult Menu(int? id, int? page,string SearchString, double min = double.MinValue, double max = double.MaxValue)
        {
            #region
            //if (id != null)
            //{
            //    return View(database.Products.Where(s => s.CategoryID == id).ToList());
            //}
            //else
            //{
            //    return View(database.Products.ToList());
            //}
            #endregion
            #region
            //var products = database.Products.Include(p => p.Category);

            //// Tìm kiếm chuỗi truy vấn theo NamePro (SearchString)
            //if (!String.IsNullOrEmpty(SearchString))
            //{
            //    products = products.Where(s => s.ProductName.Contains(SearchString.Trim()));
            //}
            //// Tìm kiếm chuỗi truy vấn theo đơn giá
            //if (min >= 0 && max > 0)
            //{
            //    products = products.Where(p => (double)p.Price >= min && (double)p.Price <= max);
            //}
            //// Tìm kiếm chuỗi truy vấn theo category
            //if (id != null)
            //{
            //    products = products.Where(s => s.CategoryID == id).OrderByDescending(x => x.ProductName);
            //}

            //return View(products.ToList());
            #endregion
            var products = database.Products.Include(p => p.Category);

            // Tìm kiếm chuỗi truy vấn theo category
            if (id != null)
            {
                products = products.Where(s => s.CategoryID == id);
            }

            // Tìm kiếm chuỗi truy vấn theo tên sản phẩm (SearchString)
            if (!string.IsNullOrEmpty(SearchString))
            {
                products = products.Where(s => s.ProductName.Contains(SearchString.Trim()));
            }

            // Tìm kiếm chuỗi truy vấn theo đơn giá
            if (min >= 0 && max > 0)
            {
                products = products.Where(p => (double)p.Price >= min && (double)p.Price <= max);
            }

            // Sắp xếp danh sách theo tên sản phẩm theo thứ tự giảm dần
            products = products.OrderByDescending(x => x.ProductName);

            // Khai báo số sản phẩm trên mỗi trang
            int pageSize = 6;

            // Trang hiện tại, nếu không có thì mặc định là 1
            int pageNumber = page ?? 1;

            // Lấy danh sách sản phẩm được phân trang
            var pagedProducts = products.ToPagedList(pageNumber, pageSize);

            return View(pagedProducts);

        }
        public ActionResult Create()
        {
            Product pro = new Product();
            return View(pro);
        }
        [HttpPost]
        public ActionResult Create(Models.Product pro)
        {
            try
            {
                if (pro.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(pro.UploadImage.FileName);
                    string extent = Path.GetExtension(pro.UploadImage.FileName);
                    filename = filename + extent;
                    pro.ImageProduct1 = "~/Content/TemplateFile/images/AddPro/" + filename;
                    pro.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/TemplateFile/images/AddPro/"), filename));
                }
                database.Products.Add(pro);
                database.SaveChanges();
                return RedirectToAction("Menu");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult SelectCate()
        {
            Category se_cate = new Category();
            se_cate.ListCate = database.Categories.ToList<Category>();
            return PartialView(se_cate);
        }
        public ActionResult Index(string _name)
        {
            if (_name == null)
                return View(database.Products.ToList());
            else
                return View(database.Products.Where(s => s.ProductName.Contains(_name)).ToList());
        }
        public ActionResult Delete(int id)
        {
            return View(database.Products.Where(s => s.ProductID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, Product pro)
        {
            try
            {
                pro = database.Products.Where(s => s.ProductID == id).FirstOrDefault();
                database.Products.Remove(pro);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data is using in other table, Error Delete");
            }
        }
        public ActionResult Edit(int id)
        {
            return View(database.Products.Where(s => s.ProductID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, Product pro)
        {
            database.Entry(pro).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var product = database.Products.Find(id);
            return View(product);
        }
    }
}
