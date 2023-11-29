using Baocao_chuyende.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baocao_chuyende.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Admin/Account
        Web_NangcaoEntities db = new Web_NangcaoEntities();
        public ActionResult Account()
        {
            List<Account> accounts = db.Accounts.Where(a => a.role == 1).ToList();
            return View(accounts);
        }
    }
}