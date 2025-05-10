using UnityEngine;
using Board;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private BoardController board;
    [SerializeField] private TurnController turn;

    [SerializeField] private List<InventoryItem> currentInventory;  
    [SerializeField] private List<ItemModel> slotsInventory;

    private void OnEnable()
    {
        board.OnStartGame += UpdateInventoryNow;
        turn.OnChangeTurn += UpdateInventory;
    }

    private void OnDisable()
    {
        board.OnStartGame -= UpdateInventoryNow;
        turn.OnChangeTurn -= UpdateInventory;
    }

    private void UpdateInventory()
    {
        currentInventory = GetCurrentInventory();
        PopulateInventory();
    }

    public void UpdateInventoryNow()
    {
        currentInventory = GetCurrentInventoryNow();
        PopulateInventory();
    }

    private List<InventoryItem> GetCurrentInventoryNow()
    {
        return board.GetCurrentPlayer().GetComponent<PlayerInventory>().Inventory;
    }

    private List<InventoryItem> GetCurrentInventory()
    {
        return board.GetNextPlayer().GetComponent<PlayerInventory>().Inventory;
    }

    private void PopulateInventory()
    {
        for (int i = 0; i < slotsInventory.Count; i++)
        {
            slotsInventory[i].Data = i < currentInventory.Count ? currentInventory[i].itemData : null;
        }
    }
}
