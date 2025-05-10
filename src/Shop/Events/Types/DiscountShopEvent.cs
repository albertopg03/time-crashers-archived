using System.Collections.Generic;
using UnityEngine;

public class DiscountShopEvent : IShopEvent
{
    public void Apply(ShopEventContext context)
    {
        List<ShopController.CurrentShopItemSlot> currentList = context.shopController.GetCurrentList();

        foreach (var item in currentList)
        {
            item.SetPrice(Mathf.FloorToInt(item.Price * 0.75f));
        }
    }
}

