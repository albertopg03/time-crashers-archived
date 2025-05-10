public class ShopItemSlotBuilder
{
    private ShopManager.ItemSlotManager _itemManager;
    private int _stock;
    private int _price;

    public ShopItemSlotBuilder SetItemManager(ShopManager.ItemSlotManager itemManager)
    {
        _itemManager = itemManager;
        _stock = itemManager.itemSlot.stock; 
        return this;
    }

    public ShopItemSlotBuilder SetStock(int stock)
    {
        _stock = stock;
        return this;
    }

    public ShopItemSlotBuilder SetPrice(int price)
    {
        _price = price;
        return this;
    }

    public ShopController.CurrentShopItemSlot Build()
    {
        return new ShopController.CurrentShopItemSlot(_itemManager, _stock);
    }
}
