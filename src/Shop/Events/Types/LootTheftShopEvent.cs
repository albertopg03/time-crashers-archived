using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LootTheftShopEvent : IShopEvent
{
    public void Apply(ShopEventContext context)
    {
        List<ShopController.CurrentShopItemSlot> currentList = context.shopController.GetCurrentList();

        List<int> randomIndexes = Enumerable.Range(0, currentList.Count)
                                            .OrderBy(x => Random.value)
                                            .Take(2)
                                            .ToList();

        currentList[randomIndexes[0]].SetStock(0);
        currentList[randomIndexes[1]].SetStock(0);
    }
}
