using Baocao_chuyende.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Baocao_chuyende.Controllers
{
    public class LoginController : Controller
    {
        private Web_NangcaoEntities db = new Web_NangcaoEntities();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if(ModelState.IsValid)
            {
                var account = db.Accounts.FirstOrDefault(a => a.username == username && a.password == password && a.role == 1);

                if (account != null)
                {
                    Session["username"] = account;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Tài khoản không đúng. \n Vui lòng kiểm tra tên đăng nhập và mật khẩu.");
                }
            }
             return View();
        }

        public ActionResult Logout()
        {
            Session.Remove("username");
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}