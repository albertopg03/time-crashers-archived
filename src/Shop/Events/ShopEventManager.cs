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
    /// Función encargada de devolver qué tipo de evento de tienda se producirá
    /// </summary>
    /// <returns></returns>
    public IShopEvent GetRandomEvent()
    {
        return availableEvents[random.Next(availableEvents.Count)];
    }
}

