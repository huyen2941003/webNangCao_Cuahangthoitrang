using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baocao_chuyende.Areas.Admin.Controllers
{
    [Authorize]
    public class ContactUsController : Controller
    {
        // GET: Admin/ContactUs
        public ActionResult Index()
        {
            return View();
        }
    }
}