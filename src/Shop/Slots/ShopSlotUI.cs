using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Clase que actúa como UI para cada uno de los slots de la tienda. Cada slot tendrá este script, y 
/// se encargará de gestionarse de forma individual su UI. Esto no tiene nada que ver con la UI como tal de la tienda.
/// </summary>
public class ShopSlotUI : MonoBehaviour
{
    private ShopItemSlot data; // datos del slot

    [Header("UI ELEMENTS")]
    [SerializeField] private Image icon;
    [SerializeField] private Button shopBtn;
    [SerializeField] private Button infoBtn;
    [SerializeField] private TMP_Text priceText;

    [Header("UI INFO")]
    [SerializeField] private ShopSlotInfoUI infoSlot;
    [SerializeField] private ShopSlotBuyUI buySlot;

    private int id;

    public void SetData(ShopItemSlot data, int id, int updatedPrice)
    {
        this.data = data;
        this.id = id;

        Init(updatedPrice);
    }

    private void OnEnable()
    {
        shopBtn.onClick.AddListener(OnBuyClicked);
        infoBtn.onClick.AddListener(OnInfoClicked);
    }

    private void OnDisable()
    {
        shopBtn.onClick.RemoveListener(OnBuyClicked);
        infoBtn.onClick.RemoveListener(OnInfoClicked);
    }

    /// <summary>
    /// Función encargada de inicializar la UI del slot
    /// </summary>
    private void Init(int updatedPrice)
    {
        icon.sprite = data.item.icon;
        priceText.text = updatedPrice.ToString();  
    }

    private void OnBuyClicked()
    {
        buySlot.Show(data, id);
    }

    private void OnInfoClicked()
    {
        infoSlot.Show(data);
    }
}
