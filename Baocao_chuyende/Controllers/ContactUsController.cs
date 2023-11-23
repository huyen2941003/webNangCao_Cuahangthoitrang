using Baocao_chuyende.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baocao_chuyende.Controllers
{
    public class ContactUsController : Controller
    {
        private Web_NangcaoEntities db = new Web_NangcaoEntities();

        public class ContactUsViewModel
        {
            public List<Product> Products { get; set; }
            public List<Category> Categories { get; set; }
            public List<Brand> Brands { get; set; }
            public ContactU ContactUs { get; set; }
            public List<InformationShop> InformationShops { get; set; }
            public List<TypeProduct> TypeProducts { get; set; }
        }
        public ActionResult ListContactUs()
        {
            List<ContactU> contactUs = db.ContactUs.ToList();
            contactUs = db.ContactUs.ToList();
            return View(contactUs);
        }

        [HttpGet]
        public ActionResult ContactUs()
        {
            var viewModel = new ContactUsViewModel
            {
                Products = db.Products.ToList(),
                Categories = db.Categories.ToList(),
                Brands = db.Brands.ToList(),
                ContactUs = new ContactU(),
                InformationShops = db.InformationShops.ToList(),
                TypeProducts = db.TypeProducts.ToList(),
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ContactUs(ContactUsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                db.ContactUs.Add(viewModel.ContactUs);
                db.SaveChanges();
                return RedirectToAction("ContactUs");
            }
            viewModel.Products = db.Products.ToList();
            viewModel.Categories = db.Categories.ToList();
            viewModel.Brands = db.Brands.ToList();

            return View(viewModel);
        }
    }

}