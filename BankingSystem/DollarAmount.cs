namespace BankingSystem;

public class DollarAmount
{
    public DollarAmount(decimal value)
    {
        this.Value = value;
    }

    public decimal Value { get; private set; }

    public DollarAmount Add(DollarAmount amount)
    {
        return new DollarAmount(this.Value + amount.Value);
    }

    public DollarAmount Subtract(DollarAmount amount)
    {
        return new DollarAmount(this.Value - amount.Value);
    }
}