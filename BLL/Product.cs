using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class Product
    {
        private int productId;
        private string productName;
        private decimal productPrice;
        private int productQtty;

        public Product(int id, string name, decimal price, int qtty)
        {
            productId = id;
            productName = name;
            productPrice = price;
            productQtty = qtty;
        }

        public int getId()
        {
            return productId;
        }
        public string getName()
        {
            return productName;
        }
        public decimal getPrice()
        {
            return productPrice;
        }
        public int getQtty()
        {
            return productQtty;
        }

        public void setQtty(int newQtty)
        {
            productQtty = newQtty;
        }
    }
}
