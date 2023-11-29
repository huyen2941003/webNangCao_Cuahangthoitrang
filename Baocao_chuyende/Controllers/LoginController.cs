using Baocao_chuyende.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            if (Request.Cookies["UserCookie"] != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                string encodedPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));

                var account = db.Accounts.FirstOrDefault(a => a.username == username && a.password == encodedPassword && a.role == 1);

                 if (account != null)
                {
                    HttpCookie authCookie = new HttpCookie("UserCookie");
                    authCookie.Values["username"] = account.username;
                    authCookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(authCookie);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Tài khoản không đúng. Vui lòng kiểm tra tên đăng nhập và mật khẩu.");
                }
            }
             return View();
        }

        public ActionResult Logout()
        {
            if (Request.Cookies["UserCookie"] != null)
            {
                HttpCookie userCookie = new HttpCookie("UserCookie");
                userCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(userCookie);
            }
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}