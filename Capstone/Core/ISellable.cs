using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core
{
    public interface ISellable
    {
        public string Name { get; }

        public decimal Price { get; }

        public string SaleMessage { get; }
    }
}
