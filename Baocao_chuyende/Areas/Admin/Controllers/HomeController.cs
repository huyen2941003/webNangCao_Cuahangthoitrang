using Baocao_chuyende.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baocao_chuyende.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        Web_NangcaoEntities db = new Web_NangcaoEntities();
        public ActionResult Index()
        {
            return View();
        }
    }
}