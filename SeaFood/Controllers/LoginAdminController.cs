using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeaFood.Models;
namespace SeaFood.Controllers
{
    public class LoginAdminController : Controller
    {
        DBSeaFoodEntities database = new DBSeaFoodEntities();
        // GET: LoginAdmin
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult AuthenLogin(AdminUser _user)
        {
            try
            {
                var check_ID = database.AdminUsers.Where(s => s.ID == _user.ID).FirstOrDefault();
                var check_Name = database.AdminUsers.Where(s => s.NameUser == _user.NameUser).FirstOrDefault();
                var check_Pass = database.AdminUsers.Where(s => s.PasswordUser == _user.PasswordUser).FirstOrDefault();
                if (check_Name == null || check_Pass == null || check_ID == null)
                {
                    if (check_ID == null)
                         ViewBag.ErrorID = "ID không đúng";
                        if (check_Name == null)
                        ViewBag.ErrorName = "Tên đăng nhập không đúng";
                    if (check_Pass == null)
                        ViewBag.ErrorPass = "Mật khẩu không đúng";
                    return View("Login");
                }
                else
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            catch
            {
                return View("Login");
            }
        }
    }
}