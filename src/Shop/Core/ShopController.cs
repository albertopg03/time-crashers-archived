using System;
using System.Collections.Generic;
using UnityEngine;
using Board;
using System.Linq;

public class ShopController : MonoBehaviour
{
    [Header("REFERENCIAS")]
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private BoardController board;
    [SerializeField] private InventoryController inventoryController;

    [Header("SHOP REFERENCES")]
    [SerializeField] private List<ShopSlotUI> listSlotsUI; // referencias a todos los slots de la tienda. Se rellena manualmente

    [SerializeField] private List<CurrentShopItemSlot> currentList;

    private ShopEventManager eventManager;

    [Serializable]
    public class CurrentShopItemSlot
    {
        public int id { get; private set; }
        public ShopManager.ItemSlotManager itemManager;

        [SerializeField] private int price;  
        public int Price => price;        

        public int stock;

        private static int nextId = 0;

        public CurrentShopItemSlot(ShopManager.ItemSlotManager manager, int stock)
        {
            id = nextId++;
            itemManager = manager;
            this.stock = stock;
            price = manager.itemSlot.price;
        }

        public void SetPrice(int newPrice)
        {
            price = newPrice;
        }

        public void SetStock(int newStock)
        {
            stock = newStock;
        }
    }


    public static event Action OnUpdatedShop;

    private void Awake()
    {
        eventManager = new ShopEventManager();
    }

    private void OnEnable()
    {
        InitShop();

        board.OnRotationTurn += InitShop;
        board.OnEventShop += ApplyRandomEvent;
    }

    private void OnDisable()
    {
        board.OnRotationTurn -= InitShop;
        board.OnEventShop -= ApplyRandomEvent;
    }

    private void InitShop()
    {
        currentList.Clear();

        foreach (var item in shopManager.GetItemSlotList())
        {
            CurrentShopItemSlot shopItem = new CurrentShopItemSlot(item, 1); 
            currentList.Add(shopItem);
        }

        PopulateShop();
    }


    private void PopulateShop()
    {
        for (int i = 0; i < listSlotsUI.Count; i++)
        {
            listSlotsUI[i].SetData(currentList[i].itemManager.itemSlot, currentList[i].id, currentList[i].Price);
        }

        OnUpdatedShop?.Invoke();
    }

    public bool BuyItem(int idSlot)
    {
        Player currentPlayer = board.GetCurrentPlayer();
        PlayerInventory inventory = currentPlayer.inventory;
        PlayerStats playerStats = currentPlayer.stats;
        PlayerState playerState = currentPlayer.state;

        if (!playerState.CanBuy) return false;

        CurrentShopItemSlot currentItem = currentList.FirstOrDefault(item => item.id == idSlot);

        if (!inventory.IsOverLimit() && playerStats.Fragments >= currentItem.Price && currentItem.stock > 0)
        {
            InventoryItem newItem = new InventoryItem(currentItem.itemManager.itemSlot.item);
            inventory.AddItem(newItem.itemData);

            currentItem.stock--;
            Debug.Log("JUGADOR " + board.GetCurrentPlayer() + " HA COMPRADO " + currentItem.itemManager.itemSlot.item.itemName);

            playerStats.Fragments -= currentItem.Price;

            UpdateShop();

            playerState.CanBuy = false;

            return true;
        }

        return false;
    }

    private void UpdateShop()
    {
        inventoryController.UpdateInventoryNow();
        PopulateShop();
    }

    public List<CurrentShopItemSlot> GetCurrentList()
    {
        return currentList;
    }

    public void ApplyRandomEvent()
    {
        InitShop(); 

        var shopEvent = eventManager.GetRandomEvent();
        var context = new ShopEventContext(this);
        shopEvent.Apply(context);

        UpdateShop();
    }
}
