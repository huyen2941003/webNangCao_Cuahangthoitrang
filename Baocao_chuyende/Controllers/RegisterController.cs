using Baocao_chuyende.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baocao_chuyende.Controllers
{
    public class RegisterController : Controller
    {
        private Web_NangcaoEntities db = new Web_NangcaoEntities();
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Account customer)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(customer);
                db.SaveChanges();

                return RedirectToAction("Login", "Login");
            }

            return View(customer);
        }
    }

}