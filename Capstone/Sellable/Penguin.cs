using Capstone.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Sellable
{
    public class Penguin : ISellable
    {
        public decimal Price { get; private set; }

        public string Name { get; private set; }

        public string SaleMessage => "Squawk, Squawk, Whee!";

        public Penguin(decimal price, string name)
        {
            Price = price;
            Name = name;
        }

        public override bool Equals(object obj)
        {
            var sellable = obj as ISellable;

            if (sellable == null)
            {
                return false;
            }
            else if (sellable.Name != Name)
            {
                return false;
            }
            else if (sellable.Price != Price)
            {
                return false;
            }
            else if (sellable.SaleMessage != SaleMessage)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
