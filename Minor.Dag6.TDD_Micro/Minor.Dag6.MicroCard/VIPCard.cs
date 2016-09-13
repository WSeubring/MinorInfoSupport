using Minor.Dag6.Micro;

public class VIPCard : Card
{
    public decimal Discount { get; private set; }

    public VIPCard(int v1, decimal balance) : base(balance)
    {
        this.Discount = v1;
    }
}