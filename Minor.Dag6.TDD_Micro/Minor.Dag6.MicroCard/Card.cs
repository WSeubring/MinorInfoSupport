using System;

namespace Minor.Dag6.Micro
{
    public class Card
    {
        public decimal Balance { get; private set; }

        public Card(decimal balance)
        {
            Balance = balance;
        }

        public void Payment(decimal ammount)
        {
            if (Balance - ammount >= 0)
            {
                Balance -= ammount;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
