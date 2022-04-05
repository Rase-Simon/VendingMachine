using BLL;
using System;
using System.Globalization;

namespace CUI
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMachine vm = new VendingMachine();
            bool running = true;
            string command;
            while(running)
            {
                Console.WriteLine(vm.showMessage());
                command = Console.ReadLine();
                if(command == "SHOW")
                {
                    vm.showProducts();
                }
                else
                {
                    if (command == "RETURN COINS")
                    {
                        vm.returnMoney();
                    }
                    else
                    {
                        int space = command.IndexOf(" ");
                        if (command.Substring(0, space) == "INSERT")
                        {
                            vm.addMoney(Convert.ToDecimal(command.Substring(space), new CultureInfo("en-US")));
                        }
                        else
                        {
                            if (command.Substring(0, space) == "SELECT")
                            {
                                vm.buyProduct(Convert.ToInt32(command.Substring(space)));
                            }
                            else
                            {
                                if(command.Substring(0, space) == "LANGUAGE")
                                {
                                    vm.setLanguage(command.Substring(space+1, 2));
                                }
                                else
                                {
                                    Console.WriteLine("Error: invalid command");
                                }
                                
                            }
                        }
                    }
                }
            }
        }
    }
}
