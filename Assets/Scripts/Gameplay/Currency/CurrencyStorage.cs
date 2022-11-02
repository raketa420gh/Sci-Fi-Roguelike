using System;

public class CurrencyStorage : ICurrencyStorage
{
    public event Action<int> OnCurrencyAmountChanged;
    
    public int Amount { get; private set; }

    public CurrencyStorage(int amount = 0)
    {
        Amount = amount;
    }

    public void ChangeAmount(int amount)
    {
        Amount += amount;

        if (Amount < 0)
            Amount = 0;
        
        OnCurrencyAmountChanged?.Invoke(Amount);
    }
}