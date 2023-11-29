using Baocao_chuyende.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Baocao_chuyende.Controllers
{
    public class RegisterController : Controller
    {
        private Web_NangcaoEntities db = new Web_NangcaoEntities();
        public ActionResult Register()
        {
            if (Request.Cookies["UserCookie"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Register(Account customer)
        {
            if (ModelState.IsValid)
            {
                string encodedPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(customer.password));
                customer.password = encodedPassword;
                customer.role = 1;
                db.Accounts.Add(customer);
                db.SaveChanges();

                return RedirectToAction("Login", "Login");
            }

            return View(customer);
        }
    }

}