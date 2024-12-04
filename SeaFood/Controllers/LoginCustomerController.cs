using SeaFood.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeaFood.Controllers
{
    public class LoginCustomerController : Controller
    {
        DBSeaFoodEntities database = new DBSeaFoodEntities();
        // GET: LoginCustomer
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult AuthenLogin(Customer _cus)
        {
            try
            {

                var check_ID = database.Customers.Where(s => s.IDCus == _cus.IDCus).FirstOrDefault();
                var check_Pass = database.Customers.Where(s => s.PasswordCus == _cus.PasswordCus).FirstOrDefault();
                if (check_ID == null || check_Pass == null)
                {
                    if (check_ID == null)
                        ViewBag.ErrorID = "Số điện thoại không đúng";
                    if (check_Pass == null)
                        ViewBag.ErrorPass = "Mật khẩu không đúng";
                    return View("Login");
                }
                else
                {
                    return RedirectToAction("Menu", "Product");
                }
            }
            catch
            {
                return View("Login");
            }
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult AuthenRegister(Customer _cus)
        {
            try
            {
                database.Customers.Add(_cus);
                database.SaveChanges();
                return RedirectToAction("Login");
            }
            catch
            {
                return View("Register");
            }
        }
        public ActionResult LogOutUser()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}