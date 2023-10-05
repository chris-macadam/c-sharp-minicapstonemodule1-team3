﻿using Capstone.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Sellable
{
    public class Cat : ISellable
    {
        public decimal Price { get; private set; }

        public string Name { get; private set; }

        public string SaleMessage => "Meow, Meow, Meow!";

        public Cat(decimal price, string name)
        {
            Price = price;
            Name = name;
        }
    }
}