using Baocao_chuyende.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baocao_chuyende.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private Web_NangcaoEntities db = new Web_NangcaoEntities();
        public ActionResult Index()
        {
            HttpCookie userCookie = Request.Cookies["UserCookie"];
            if (userCookie != null)
            {
                string username = userCookie["username"];
                var account = db.Accounts.FirstOrDefault(a => a.username == username);

                if (account != null)
                {
                    int accountId = account.id;
                    var accountData = db.Accounts.Where(data => data.id == accountId).ToList();
                    ViewBag.AccountData = accountData;
                    return View();
                }
            }
            return RedirectToAction("Login", "Login");
        }
    }
}