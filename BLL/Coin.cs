using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class Coin
    {
        private decimal coinValue;
        private int coinQtty;

        public Coin(decimal value, int qtty)
        {
            coinValue = value;
            coinQtty = qtty;
        }

        public int getQtty()
        {
            return coinQtty;
        }

        public void setQtty(int qtty)
        {
            coinQtty = qtty;
        }

        public decimal getValue()
        {
            return coinValue;
        }
    }
}
