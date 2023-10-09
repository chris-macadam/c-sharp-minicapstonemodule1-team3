using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core
{
    public class InventorySlot
    {
        public const int MaxAmount = 5;

        public string Name { get; private set; }

        public ISellable Item { get; private set; }

        public int CurrentAmount { get; set; }

        public bool IsSoldOut => CurrentAmount <= 0;

        public InventorySlot(string name, ISellable item)
        {
            Name = name;
            Item = item;
            CurrentAmount = MaxAmount;
        }

        public override string ToString()
        {
            return $"{Item.Name} {Name}";
        }

        public override bool Equals(object obj)
        {
            var slot = obj as InventorySlot;

            if(slot == null)
            {
                return false;
            }
            else if(slot.Name != Name)
            {
                return false;
            }
            else if(slot.CurrentAmount != CurrentAmount)
            {
                return false;
            }
            else if (!slot.Item.Equals(Item))
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
