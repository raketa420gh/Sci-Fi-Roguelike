using System;

public interface ICurrencyStorage
{
    event Action<int> OnCurrencyAmountChanged;

    int Amount { get; }

    void ChangeAmount(int amount);
}