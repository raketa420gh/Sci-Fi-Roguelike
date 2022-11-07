public interface ITrader : IInteractable
{
    void LeaveTrader();
    void Sell(IInventoryItem salableItem, CurrencyStorage currencyStorage, int cost);
}