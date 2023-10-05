﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Capstone.FileIO;

namespace Capstone.Core
{
    public class VendingMachine
    {
        public Dictionary<string, InventorySlot> Slots { get; private set; } = new Dictionary<string, InventorySlot>();
        public Dictionary<string, int> ItemsSold { get; private set; } = new Dictionary<string, int>();
        public VendingMachineDisplay Display { get; private set; } 
        public decimal Profit { get; private set; }
        public decimal UserBalance { get; private set; }

        public VendingMachine(Dictionary<string,InventorySlot> slots)
        {
            Slots = slots;
            Display = new VendingMachineDisplay(this);
            using (StreamWriter sw = new StreamWriter("TransactionLog.txt"))
            {
                sw.Write("");
            }
        }

        public void FeedMoney(decimal amount)
        {
            UserBalance += amount;
            Transaction transaction = new Transaction("FEED MONEY:", amount, UserBalance);
            VendingMachineFileIO.UpdateTransactionLog(transaction);
        }

        public Dictionary<string,int> GiveChange()
        {
            decimal amount = UserBalance;
            Dictionary<string, int> coinCount = new Dictionary<string, int>() 
            {
                {"quarter", 0 },
                {"dime", 0 },
                {"nickle", 0 },
                {"penny", 0 }
            };

            //do logic

            //update balance
            UserBalance = 0;

            Transaction transaction = new Transaction("GIVE CHANGE:", amount, UserBalance);
            VendingMachineFileIO.UpdateTransactionLog(transaction);

            return coinCount;
        }

        public string SellItem(string slotName)
        {
            InventorySlot slot = null;

            if (Slots.ContainsKey(slotName))
            {
                slot = Slots[slotName];
            }

            if(slot == null)
            {
                return "INVALID SLOT";
            }
            else if (slot.IsSoldOut)
            {
                return "SOLD OUT";
            }
            else
            {
                if(slot.Item.Price > UserBalance)
                {
                    return "INSUFFICIENT FUNDS";
                }
                else
                {
                    UserBalance -= slot.Item.Price;
                    Profit += slot.Item.Price;
                    slot.CurrentAmount -= 1;

                    if (ItemsSold.ContainsKey(slot.Item.Name))
                    {
                        ItemsSold[slot.Item.Name] += 1;
                    }
                    else
                    {
                        ItemsSold[slot.Item.Name] = 1;
                    }

                    Transaction transaction = new Transaction(slot.ToString(), slot.Item.Price, UserBalance);
                    VendingMachineFileIO.UpdateTransactionLog(transaction);

                    return slot.Item.SaleMessage;
                }
            }
        } 
    }
}
