using Baocao_chuyende.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baocao_chuyende.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductDetailController : Controller
    {
        Web_NangcaoEntities db = new Web_NangcaoEntities();
        // GET: Admin/ProductDetails
        public ActionResult ProductDetail()
        {
            List<ProductDetail> product = db.ProductDetails.ToList();
            return View(product);
        }
    }
}