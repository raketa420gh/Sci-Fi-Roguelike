public interface IBuyer
{
    void Buy(IInventoryItem purchasedItem, ICurrencyStorage currencyStorage, int cost);
}