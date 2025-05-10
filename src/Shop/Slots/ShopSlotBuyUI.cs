using UnityEngine;
using UnityEngine.UI;

public class ShopSlotBuyUI : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField] private ShopController shopController;

    [Header("UI ELEMENTS")]
    [SerializeField] private Image iconItem;
    [SerializeField] private Button buyBtn;

    [Space(5)]
    [SerializeField] private Button closeButton;

    private int idSlot;

    private void OnEnable()
    {
        closeButton.onClick.AddListener(HideInfo);
        buyBtn.onClick.AddListener(ConfirmBuy);
    }

    private void OnDisable()
    {
        closeButton.onClick.RemoveListener(HideInfo);
        buyBtn.onClick.RemoveListener(ConfirmBuy);
    }

    public void Show(ShopItemSlot itemData, int idSlot)
    {
        iconItem.sprite = itemData.item.icon;

        this.idSlot = idSlot;

        gameObject.SetActive(true);
    }

    private void ConfirmBuy()
    {
        if(shopController.BuyItem(idSlot)) HideInfo();
    }

    private void HideInfo()
    {
        gameObject.SetActive(false);
    }
}
