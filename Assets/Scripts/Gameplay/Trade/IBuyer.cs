using System;

public interface IBuyer
{
    event Action<ITrader> OnTradingStarted;
    event Action OnTradingFinished;
    event Action<IInventoryItem> OnBought;

    void FinishTrading();

    void Buy(IInventoryItem item);
}