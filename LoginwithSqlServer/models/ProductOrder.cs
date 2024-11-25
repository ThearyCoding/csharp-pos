using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginwithSqlServer
{
    public class ProductOrder
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
        public int Quantity { get; set; }
        public double Subtotal { get; set; }

        public ProductOrder(string productId, string productName, string productPrice, int quantity, double subtotal)
        {
            ProductId = productId;
            ProductName = productName;
            ProductPrice = productPrice;
            Quantity = quantity;
            Subtotal = subtotal;
        }
    }

}
