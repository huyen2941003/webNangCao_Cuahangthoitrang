using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Baocao_chuyende.Models
{
    public class CartModel
    {
        [DisplayName("Mã sản phẩm")]
        public int ProductId { get; set; }
        [DisplayName("Thông tin sản phẩm")]
        public Product Products { get; set; }
        [DisplayName("Ảnh")]
        public ProductDetail ProductDetails { get; set; }
        [DisplayName("Số lượng")]
        public int Quantity { get; set; } = 0;
        [DisplayName("Thành tiền")]
        public double Amount { get; set; } = 0;

    }
}