using System;

public class JunkItem : IInventoryItem
{
    public IInventoryItemInfo Info { get; }
    public IInventoryItemState State { get; }
    public Type Type => GetType();

    public JunkItem(IInventoryItemInfo info)
    {
        Info = info;
        State = new InventoryItemState();
    }
    
    public IInventoryItem Clone() => 
        new JunkItem(Info) { State = { Amount = State.Amount } };
}