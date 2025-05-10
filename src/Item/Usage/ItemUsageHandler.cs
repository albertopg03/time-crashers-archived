using UnityEngine;
using Board;

public class ItemUsageHandler
{
    private BoardController board;
    private ItemModel model;

    public ItemUsageHandler(BoardController board, ItemModel model)
    {
        this.board = board;
        this.model = model;
    }

    public void HandleItemUse()
    {
        Player currentPlayer = board.GetCurrentPlayer();
        PlayerInventory inventory = currentPlayer.GetComponent<PlayerInventory>();

        // Buscar el ítem en el inventario del jugador basado en el ItemData
        InventoryItem itemToUse = inventory.Inventory.Find(i => i.itemData == model.Data);

        itemToUse.itemData.effect.ApplyEffect(currentPlayer);

        // Remover el ítem del inventario usando su ID
        inventory.Remove(itemToUse.uniqueID);
    }
}
