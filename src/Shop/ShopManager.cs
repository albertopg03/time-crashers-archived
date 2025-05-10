using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private List<ItemSlotManager> itemList;

    [System.Serializable]
    public class ItemSlotManager
    {
        public ShopItemSlot itemSlot;
    }

    public List<ItemSlotManager> GetAllItemSlotList()
    {
        return itemList;
    }

    /// <summary>
    /// Función encargada de devolver una lista de elementos NO repetidos
    /// </summary>
    /// <returns></returns>
    public List<ItemSlotManager> GetItemSlotList()
    {
        // Separar los ítems según su rareza
        var commonItems = itemList.Where(item => item.itemSlot.rarity == ShopItemSlot.Rarity.Common).OrderBy(x => Random.value).ToList();
        var rareItems = itemList.Where(item => item.itemSlot.rarity == ShopItemSlot.Rarity.Rare).OrderBy(x => Random.value).ToList();
        var epicItems = itemList.Where(item => item.itemSlot.rarity == ShopItemSlot.Rarity.Epic).OrderBy(x => Random.value).ToList();

        // Seleccionar los elementos asegurando que no se repitan en cada categoría
        List<ItemSlotManager> selectedItems = new List<ItemSlotManager>();

        if (commonItems.Count >= 3) selectedItems.AddRange(commonItems.Take(3));
        if (rareItems.Count >= 2) selectedItems.AddRange(rareItems.Take(2));
        if (epicItems.Count >= 1) selectedItems.AddRange(epicItems.Take(1));

        // Si queremos que la lista final tenga orden aleatorio:
        selectedItems = selectedItems.OrderBy(x => Random.value).ToList();

        return selectedItems;
    }

}
