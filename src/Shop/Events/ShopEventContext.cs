using UnityEngine;

public class ShopEventContext
{
    public ShopController shopController { get; set; }

    public ShopEventContext(ShopController shopController)
    {
        this.shopController = shopController;
    }
}
