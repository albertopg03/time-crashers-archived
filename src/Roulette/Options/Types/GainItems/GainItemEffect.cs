using UnityEngine;

public class GainItemEffect : EffectOptionRoulete
{
    [Header("DATA")]
    [Space(5)]
    public OptionModel model;

    private void Awake()
    {
        model = GetComponent<OptionModel>();
    }

    public override void ApplyEffect(Player player)
    {
        PlayerInventory inventory = player.GetComponent<PlayerInventory>();

        InventoryItem newItem = new InventoryItem(model.data.item); 

        inventory.AddItem(newItem.itemData);
    }
}
