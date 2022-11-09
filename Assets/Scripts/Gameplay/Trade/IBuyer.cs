using System;

public interface IBuyer
{
    event Action<ITrader> OnTradingStarted;
    event Action OnTradingFinished;

    void FinishTrading();
}