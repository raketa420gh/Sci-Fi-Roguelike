public interface ITrader : IInteractable
{
    void StartTrading(IBuyer buyer);
    void FinishTrading();
}