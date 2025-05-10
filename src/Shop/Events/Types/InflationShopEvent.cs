using System.Collections.Generic;
using UnityEngine;

public class InflationShopEvent : IShopEvent
{
    public void Apply(ShopEventContext context)
    {
        List<ShopController.CurrentShopItemSlot> currentList = context.shopController.GetCurrentList();

        foreach (var item in currentList)
        {
            item.SetPrice(Mathf.FloorToInt(item.Price * 1.25f)); 
        }
    }
}
