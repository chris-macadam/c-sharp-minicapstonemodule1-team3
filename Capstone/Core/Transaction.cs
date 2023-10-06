using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core
{
    public class Transaction
    {
        public DateTime TimeOfTransaction { get; private set; }

        public string TransactionType { get; private set; }

        public decimal TransactionAmount { get; private set; }

        public decimal Balance { get; private set; }

        public Transaction(string transactionType, decimal transactionAmount, decimal balance)
        {
            TimeOfTransaction = DateTime.Now;
            TransactionType = transactionType;
            TransactionAmount = transactionAmount;
            Balance = balance;
        }

        public override string ToString()
        {
            return $"{TimeOfTransaction} {TransactionType} {TransactionAmount.ToString("C")} {Balance.ToString("C")}";
        }
    }
}
