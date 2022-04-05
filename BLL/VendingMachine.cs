using System;

namespace BLL
{
    public class VendingMachine
    {
        private string message;
        private decimal insertedMoney;
        private int selectedLanguage;
        private Language[] languages =
            {
                new Language(0, "EN"),
                new Language(1, "FR"),
                new Language(2, "DE")
            };
        private Coin[] acceptedCoins =
            {
                new Coin(0.05m, 1),
                new Coin(0.10m, 1),
                new Coin(0.20m, 1),
                new Coin(0.50m, 1),
                new Coin(1.00m, 1),
                new Coin(2.00m, 1)
            };
        private Coin[] insertedCoins =
            {
                new Coin(0.05m, 0),
                new Coin(0.10m, 0),
                new Coin(0.20m, 0),
                new Coin(0.50m, 0),
                new Coin(1.00m, 0),
                new Coin(2.00m, 0)
            };
        private Product[] products =
            {
                new Product(1, "COLA", 1.00m, 15),
                new Product(2, "Chips", 0.50m, 10),
                new Product(3, "Candy", 0.65m, 20),
                new Product(4, "TestNoQtty", 0.30m, 0)
            };
        private string[] insertMsg =
            { "\nINSERT COIN", "\nINSERER PIECE", "\nMUNZE EINWERFEN"};
        private string[] exactChangeMsg =
           { "\nEXACT CHANGE ONLY", "\nMONNAIE EXACTE UNIQUEMENT", "\nNUR GENAUE ANDERUNG" };
        private string[] priceMsg =
            { "PRICE :", "PRIX :", "\nPREIS :" };
        private string[] soldOutMsg =
            { "SOLD OUT", "RUPTURE DE STOCK", "\nAUSVERKAUFT" };
        private string[] leftMsg =
            {" Items Left\n", " Restants\n", "Artikel Ubrig\n" };
        private string[] currentAmountMsg =
            { "\nAmount entered: ", "\nMontant entré: ", "\nBetrag eingegeben" };
        private string[] buyingMsg =
            { "\nHere is your ", "\nVoici votre ", "\nHier ist dein " };
        private string[] changeMsg =
            { "\nHere is your change: ", "\nVoici votre monnaie: ", "\nHier ist dein Wechselgeld" };
        private string[] thanksMsg =
            { "\nThank you!", "\nMerci!", "\nDank!" };
        private string[] returnMsg =
            { "\nThe inserted money is returned", "\nLa monnaie entrée est rendue", "\nDas eingeworfene Geld wird zuruckerstattet" };
        private string[] languageMsg =
            { "\nLanguage set", "\nLangue changée", "\nNeuer Sprachsatz"};
        private string[] invalidMsg =
            { "\nError: invalid command", "\nErreur: commande invalide", "\nFehler: ungultiger Befehl"};

        public string showMessage()
        {
            return message;
        }
        public VendingMachine()
        {
            selectedLanguage = 0;
            message = insertMsg[selectedLanguage];
        }

        public void resetInsertedMoney()
        {
            insertedMoney = 0;
            for(int i = 0; i < insertedCoins.Length; i++)
            {
                acceptedCoins[i].setQtty(acceptedCoins[i].getQtty() + insertedCoins[i].getQtty());
                insertedCoins[i].setQtty(0);
            }
        }

        public decimal getInsertedMoney()
        {
            return insertedMoney;
        }

        public bool checkAddedAmount(decimal addedAmount)
        {
            bool correctCoin = false;

            for(int i=0; i<acceptedCoins.Length; i++)
            {
                if(addedAmount == acceptedCoins[i].getValue())
                {
                    correctCoin = true;
                    insertedCoins[i].setQtty(insertedCoins[i].getQtty() + 1);
                }
            }

            return correctCoin;
        }

        public decimal addMoney(decimal addedAmount)
        {
            if(checkAddedAmount(addedAmount))
            {
                insertedMoney += addedAmount;
            }

            message = currentAmountMsg[selectedLanguage] + insertedMoney;
            return insertedMoney;
        }

        public void returnMoney()
        {
            resetInsertedMoney();
            message = returnMsg[selectedLanguage] + insertMsg[selectedLanguage];
        }

        public int checkProduct(int selectedProduct)
        {
            int productIndex = -1;
            for(int i = 0; i<products.Length; i++)
            {
                if(selectedProduct == products[i].getId())
                {
                    productIndex = i;
                }
            }

            return productIndex;
        }

        public bool checkQtty(int selectedProduct)
        {
            return products[selectedProduct].getQtty() > 0;
        }

        public bool checkPrice(int selectedProduct)
        {
            bool enoughMoney = (insertedMoney >= products[selectedProduct].getPrice());

            return enoughMoney;
        }

        public bool checkChange(int selectedProduct)
        {
            bool enoughChange = true;

            decimal changeNeeded = insertedMoney - products[selectedProduct].getPrice();

            if(changeNeeded!=0)
            {
                enoughChange = false;

                for(int i = insertedCoins.Length-1; i >= 0; i--)
                {
                    while(acceptedCoins[i].getQtty()>0 && acceptedCoins[i].getValue() <= changeNeeded)
                    {
                        changeNeeded -= acceptedCoins[i].getValue();
                        acceptedCoins[i].setQtty(acceptedCoins[i].getQtty() - 1);
                    }
                }

                if(changeNeeded == 0)
                {
                    enoughChange = true;
                }
            }

            return enoughChange;
        }

        public void buyProduct(int selectedProduct)
        {
            int productIndex = checkProduct(selectedProduct);

            if(checkQtty(productIndex))
            {
                if (checkPrice(productIndex))
                {
                    if (checkChange(productIndex))
                    {
                        insertedMoney -= products[productIndex].getPrice();
                        products[productIndex].setQtty(products[productIndex].getQtty() - 1);
                        message = buyingMsg[selectedLanguage] +
                                  products[productIndex].getName();
                        if (insertedMoney != 0)
                        {
                            message += changeMsg[selectedLanguage] + insertedMoney;
                        }
                        message += thanksMsg[selectedLanguage] + insertMsg[selectedLanguage];
                        resetInsertedMoney();
                    }
                    else
                    {
                        message = exactChangeMsg[selectedLanguage];
                    }
                }
                else
                {
                    message = priceMsg[selectedLanguage] + products[productIndex].getPrice();
                }
            }
            else
            {
                message = soldOutMsg[selectedLanguage];
                if(insertedMoney!=0)
                {
                    message += currentAmountMsg[selectedLanguage] + insertedMoney;
                }
                else
                {
                    message += insertMsg[selectedLanguage];
                }
            }
        }

        public void showProducts()
        {
            string qtty = "";
            for(int i = 0; i<products.Length; i++)
            {
                if(products[i].getQtty() == 0)
                {
                    qtty = soldOutMsg[selectedLanguage];
                }
                else
                {
                    qtty = products[i].getQtty() + leftMsg[selectedLanguage];
                }
                Console.WriteLine($"{products[i].getName()} {products[i].getPrice()} euros - {qtty}");
            }

            message = insertMsg[selectedLanguage];
        }

        public void setLanguage(string language)
        {
            bool languageFound = false;
            for(int i = 0; i < languages.Length; i++)
            {
                if(language == languages[i].getName())
                {
                    selectedLanguage = i;
                    languageFound = true;
                }
            }

            if(languageFound)
            {
                message = languageMsg[selectedLanguage];
            }
            else
            {
                message = invalidMsg[selectedLanguage];
            }
        }
    }
}
