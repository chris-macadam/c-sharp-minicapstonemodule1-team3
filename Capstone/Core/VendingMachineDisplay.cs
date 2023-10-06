using Capstone.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core
{
    public class VendingMachineDisplay
    {
        public VendingMachine Machine { get; private set; }

        public VendingMachineDisplay(VendingMachine machine)
        {
            Machine = machine;
        }

        public void DisplayInventory()
        {
            string output = "";
            foreach(KeyValuePair<string,InventorySlot> slot in Machine.Slots)
            {
                output += $"{slot.Key}: {slot.Value.Item.Name} | {slot.Value.Item.Price.ToString("C")} | {slot.Value.CurrentAmount} / {InventorySlot.MaxAmount}\n";
            }
            Console.Write(output);
        }

        public void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("(1) Display Vending Machine Items\n(2) Purchase\n(3) Exit");
            bool isValidInput = false;
            int choice;
            string keyPressed;
            do
            {
                keyPressed = Console.ReadKey(true).KeyChar.ToString();
                isValidInput = int.TryParse(keyPressed, out choice) && choice > 0 && choice < 5;
            } while (!isValidInput);

            if(choice == 1)
            {
                Console.Clear();
                DisplayInventory();
                Console.ReadKey(true);
                MainMenu();
            }
            else if(choice == 2)
            {
                PurchaseMenu();
            }
            else if (choice == 3)
            {
                return;
            }
            else if(choice == 4)
            {
                Console.Clear();
                Console.WriteLine(VendingMachineFileIO.CreateSalesReport(Machine));
                Console.ReadKey(true);
                MainMenu();
            }
        }

        public void PurchaseMenu()
        {
            Console.Clear();
            Console.WriteLine($"Current Money Provided: ${Machine.UserBalance}\n");
            Console.WriteLine("(1) Feed Money\n(2) Select Product\n(3) Finish Transaction");
            bool isValidInput = false;
            int choice;
            string keyPressed;
            do
            {
                keyPressed = Console.ReadKey(true).KeyChar.ToString();
                isValidInput = int.TryParse(keyPressed, out choice) && choice > 0 && choice < 4;
            } while (!isValidInput);
            if (choice == 1)
            {
                FeedMoneyMenu();
                PurchaseMenu();
            }
            else if (choice == 2)
            {
                SelectProductMenu();
                Console.ReadKey(true);
                PurchaseMenu();
            }
            else if (choice == 3)
            {
                FinishTransaction();
                Console.ReadKey(true);
                MainMenu();
            }
        }

        public void FeedMoneyMenu()
        {
            Console.Clear();
            Console.WriteLine("How much money would you like to enter? (Dollar Amount)");
            string input = "";
            int dollarAmount;
            bool isValidInput;
            do
            {
                Console.Write("$");
                input = Console.ReadLine();
                isValidInput = int.TryParse(input, out dollarAmount) && dollarAmount > 0;
                if (!isValidInput)
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop);
                }
            }while (!isValidInput);

            Machine.FeedMoney(dollarAmount);
            
        }

        public void SelectProductMenu()
        {
            Console.Clear();
            DisplayInventory();
            Console.WriteLine("\nEnter Code Of Desired Item: ");
            string userInput = Console.ReadLine();

            string output = Machine.SellItem(userInput);
            Console.WriteLine(output);
        }

        public void FinishTransaction()
        {
            Console.Clear();
            Dictionary <string, int> coinCount = Machine.GiveChange();
            //display coins
            string output = "";
            foreach(KeyValuePair<string,int> count in coinCount)
            {
                output += $"{count.Key}(s): {count.Value}\n";
            }
            Console.Write(output);
        }
    }
}
