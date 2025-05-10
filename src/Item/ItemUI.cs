using System;
using UnityEngine;
using UnityEngine.UI;
using Board;

[RequireComponent(typeof(ItemModel))]
public class ItemUI : MonoBehaviour
{
    private ItemModel model;

    [Header("REFERENCES")]
    [Space(10)]
    [SerializeField] private BoardController board;
    [SerializeField] private InventoryController inventoryController;

    [Header("UI")]
    [Space(10)]
    [SerializeField] private Button btn;
    [SerializeField] private Image image;

    private ItemUsageHandler itemUsageHandler;

    private void OnEnable()
    {
        model = GetComponent<ItemModel>();
        itemUsageHandler = new ItemUsageHandler(board, model); // Inicializa la lógica de uso de ítem

        model.OnDataChanged += LoadUI;
        LoadUI();

        btn.onClick.AddListener(OnItemClicked);
    }

    private void OnDisable()
    {
        model.OnDataChanged -= LoadUI;
        btn.onClick.RemoveListener(OnItemClicked);
    }

    private void LoadUI()
    {
        if (model != null && model.Data != null)
        {
            image.sprite = model.Data.icon;
            image.gameObject.SetActive(true);
            btn.interactable = true;
        }
        else
        {
            image.gameObject.SetActive(false);
            btn.interactable = false;
        }
    }

    private void OnItemClicked()
    {
        itemUsageHandler.HandleItemUse();

        inventoryController.UpdateInventoryNow();
    }
}
