using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeaFood.Models
{
    public class ViewModel
    {
        public string ProductName { get; set; }
        public string ImageProduct1 { get; set; }
        public decimal? Price { get; set; }
        public string CategoryName { get; set; }
        [System.ComponentModel.DataAnnotations.Key]
        public int? ProductID { get; set; }
        public decimal Total_Money { get; set; }
        public Product product { get; set; }
        public Category category { get; set; }
        public OrderDetail orderDetail { get; set; }
        public IEnumerable<Product> ListProduct { get; set; }
        public int? Top5_Quantity { get; set; }
        public int? Sum_Quantity { get; set; }
    }
}