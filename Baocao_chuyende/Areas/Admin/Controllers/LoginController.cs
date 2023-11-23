using Baocao_chuyende.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Baocao_chuyende.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        Web_NangcaoEntities db = new Web_NangcaoEntities();
        // GET: Admin/Login

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Account data, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                int count = db.Accounts.Count(x=>x.username == data.username && x.password == data.password && x.role == 0);
                if(count == 1)
                {
                    FormsAuthentication.SetAuthCookie(data.username, false);
                    if(returnUrl == null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng");
                }
            }
            return View(data);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}