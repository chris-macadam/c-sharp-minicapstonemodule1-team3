# Vending Machine Application

## Classes

### Vending Machine
- Dictionary of possible slots <string slotName, InventorySlot slot>
- Dictionary itemsSold <string itemName, int itemsSold>
- Decimal profit
- Decimal balance
- On construction of Vending Machine, read from the inventory file and populate with slot objects and ISellable objects

public void DisplayInventory
- write out all the slots and what is in each slot

public void FeedMoney(decimal transactionAmmount)
- Transaction transaction = new Transaction("FEEDMONEY", transactionAmmount, balance)
- balance += transactionAmmount
- call UpdateTransactionLog

public void GiveChange(balance)
- Transaction transaction = new Transaction("GIVECHANGE", balance, 0.00)
- //make new dictionary to keep track of coin counts
- //do logic for coins, and edit coin dictionary accordingly
- //set balance to 0
- //call UpdateTransactionLog
- print string that displays coin dictionary contents

public void SellItem(string slotName)
- Slot slot = slotsDictionary[slotName]
- Transaction transaction = new Transaction(slot.ToString(), slot.item.price, balance)
- //check if slot exists
- //if doesnt exist, return does not exist

- //check slot for available thing
- //if unavailabe, print out of stock

- //if available, balance -= thing price and proift += thing price and slotinventory -= 1
- //if transaction goes through, print slot.item.soldMessage
- ///call UpdateTransactionLog
- //update itemSold dictionary

CreateSalesReport()
- //create the report file
- //use itemsSold to add to the file

UpdateTransactionLog()
- Pass in transaction
- print the transaction ToString to file

### InventorySlot
- string name
- ISellable item
- int currentAmmount
- const int maxAmmount = 5
- bool isSoldOut

ToString()
return $"{itemType.productName} {name}"

### Duck : ISellable
- string productName
- decimal price
- string soldMessage = "Quack, Quack, Splash!"

### Pony : ISellable
- string productName
- decimal price
- string soldMessage = "Neigh, Neight, Yay!"

### Cat : ISellable
- string productName
- decimal price
- string soldMessage = "Meow, Meow, Meow!"

### Penguin : ISellable
- string productName
- decimal price
- string soldMessage = "Squawk, Squawk, Whee!"

### Transaction
- DateTime timeOfTransaction //set in constructor to now
- decimal transactionAmmount //passed in via constructor
- decimal balance //passed in via constructor

ToString(){ return $"{timeOfTransaction} {transactionType} {transactionAmmount} {balance}"}

### Static VendingMachineBuilder (or giant method in VendingMachine)
- reads the text file, and does the populating of the vending machine, instantiates slot objects and ISellabel objects

## Interfaces

### ISellable
- decimal price
- string productName
- string soldMessage