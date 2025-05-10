using System.Collections.Generic;
using UnityEngine;

public class ShopEventManager
{
    private List<IShopEvent> availableEvents;
    private System.Random random = new System.Random();

    public ShopEventManager()
    {
        availableEvents = new List<IShopEvent>
        {
            //new DiscountShopEvent(),
            //new InflationShopEvent(),
            //new LootTheftShopEvent(),
            new FrozenShopEvent()
        };
    }

    /// <summary>
    /// Funci�n encargada de devolver qu� tipo de evento de tienda se producir�
    /// </summary>
    /// <returns></returns>
    public IShopEvent GetRandomEvent()
    {
        return availableEvents[random.Next(availableEvents.Count)];
    }
}

