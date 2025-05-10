using System;

[Serializable]
public class InventoryItem
{
    public ItemData itemData; 
    public string uniqueID;    

    public InventoryItem(ItemData data)
    {
        itemData = data;
        uniqueID = Guid.NewGuid().ToString();
    }
}
