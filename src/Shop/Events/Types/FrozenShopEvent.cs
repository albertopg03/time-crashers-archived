using System.Collections.Generic;

public class FrozenShopEvent : IShopEvent
{
    public void Apply(ShopEventContext context)
    {
        List<ShopController.CurrentShopItemSlot> currentList = context.shopController.GetCurrentList();

        foreach (var item in currentList)
        {
            item.SetStock(0);
        }
    }
}
