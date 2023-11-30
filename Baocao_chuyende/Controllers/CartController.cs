using Baocao_chuyende;
using Baocao_chuyende.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baocao_chuyende.Controllers
{
    public class CartController : Controller
    {
        private Models.Web_NangcaoEntities db = new Models.Web_NangcaoEntities();

        public class CartViewModel
        {
            public List<Product> Products { get; set; }
            public List<Category> Categories { get; set; }
            public List<Brand> Brands { get; set; }
            public List<TypeProduct> TypeProducts { get; set; }
            public List<ProductDetail> ProductDetails { get; set; }
            public List<Size> Sizes { get; set; }
            public List<Color> Colors { get; set; }

        }
        public ActionResult Cart()
        {
            var viewModel = new CartViewModel
            {
                Products = db.Products.ToList(),
                Categories = db.Categories.ToList(),
                Brands = db.Brands.ToList(),
                TypeProducts = db.TypeProducts.ToList(),
            };
            return View(viewModel);
        }
        public ActionResult AddToCart(int? id)
        {
            if (id != null)
            {
                var objproduct = db.Products.FirstOrDefault(x => x.id == id);
                if (objproduct != null)
                {
                    var objProductDetail = db.ProductDetails.FirstOrDefault(pd => pd.idProduct == id);
                    List<Models.CartModel> lstcard = null;
                    if (Session["Cart"] != null)
                    {
                        lstcard = (List<Models.CartModel>)Session["Cart"];
                    }
                    else
                    {
                        lstcard = new List<Models.CartModel>();
                    }

                    Models.CartModel check = lstcard.FirstOrDefault(m => m.ProductId == id);
                    if (check != null)
                    {
                        check.Quantity = check.Quantity + 1;
                        check.Amount = (double)(check.Quantity * check.Products.price);
                    }
                    else
                    {
                        Models.CartModel newCart = new Models.CartModel
                        {
                            ProductId = (int)id,
                            Products = objproduct,
                            ProductDetails = objProductDetail,
                            Quantity = 1,
                            Amount = (double)objproduct.price,
                        };
                        lstcard.Add(newCart);
                    }
                    Session["Cart"] = lstcard;
                }
            }
            return RedirectToAction("Cart");
        }
        public ActionResult UpDateCart(int id)
        {
            var objProduct = db.Products.FirstOrDefault(x => x.id == id);

            if (objProduct != null)
            {
                List<Models.CartModel> lstCart = Session["Cart"] as List<Models.CartModel> ?? new List<Models.CartModel>();
                var existingItem = lstCart.FirstOrDefault(item => item.ProductId == id);
                if (existingItem != null)
                {
                    existingItem.Quantity -= 1;
                    existingItem.Amount = (double)existingItem.Products.price * existingItem.Quantity;
                }
                else
                {
                    Models.CartModel newCart = new Models.CartModel
                    {
                        ProductId = (int)id,
                        Products = objProduct,
                        Quantity = 1,
                        Amount = (double)objProduct.price,
                    };
                    lstCart.Add(newCart);
                }

                Session["Cart"] = lstCart;
            }

            return RedirectToAction("Cart");
        }

        public ActionResult RemoveFromCart(int id)
        {
            var objProduct = db.Products.FirstOrDefault(x => x.id == id);
            if (objProduct != null)
            {
                List<Models.CartModel> lstCart = Session["Cart"] as List<Models.CartModel> ?? new List<Models.CartModel>();
                var itemToRemove = lstCart.FirstOrDefault(item => item.ProductId == id);
                if (itemToRemove != null)
                {
                    lstCart.Remove(itemToRemove);
                    Session["Cart"] = lstCart;
                }
            }

            return RedirectToAction("Cart");
        }

    }
}