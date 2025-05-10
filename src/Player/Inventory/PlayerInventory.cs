using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int limitSize;

    [SerializeField]
    private List<InventoryItem> inventory = new List<InventoryItem>();

    public List<InventoryItem> Inventory => inventory;

    /// <summary>
    /// Función encargada de agregar un ítem al inventario sin comprobación de Tempus ni nada. Lo agrega directamente
    /// </summary>
    /// <param name="itemData"></param>
    public void AddItem(ItemData itemData)
    {
        if (IsOverLimit()) return;

        inventory.Add(new InventoryItem(itemData));
    }

    /// <summary>
    /// Función encargada de eliminar directamente un ítem del inventario del jugador
    /// </summary>
    /// <param name="uniqueID"></param>
    public void Remove(string uniqueID)
    {
        if (IsEmpty()) return;

        InventoryItem itemToRemove = inventory.Find(i => i.uniqueID == uniqueID);

        inventory.Remove(itemToRemove);
    }

    public bool IsOverLimit()
    {
        return inventory.Count >= limitSize;
    }

    private bool IsEmpty()
    {
        return inventory.Count <= 0;
    }
}
