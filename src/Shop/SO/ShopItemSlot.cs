using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(menuName = "Shop/Item Slot", order = 1)]
public class ShopItemSlot : ScriptableObject
{
    public ItemData item;
    public int price;
    public int stock;
    public Rarity rarity;
    [TextArea(2, 5)] public string description;
    public VideoClip tutorialClip;
    
    public enum Rarity
    {
        Common,
        Rare,
        Epic
    }
}
