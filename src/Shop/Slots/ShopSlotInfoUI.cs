using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;

public class ShopSlotInfoUI : MonoBehaviour
{
    [Header("UI ELEMENTS")]
    [SerializeField] private TMP_Text nameItem;
    [SerializeField] private TMP_Text rarity;
    [SerializeField] private TMP_Text description;
    [SerializeField] private VideoPlayer videoPlayer;

    [Space(5)]
    [SerializeField] private Button closeButton;

    private void OnEnable()
    {
        closeButton.onClick.AddListener(HideInfo);
    }

    private void OnDisable()
    {
        closeButton.onClick.RemoveListener(HideInfo);
    }

    public void Show(ShopItemSlot itemData)
    {
        nameItem.text = itemData.item.itemName;
        rarity.text = itemData.rarity.ToString();
        description.text = itemData.description;
        videoPlayer.clip = itemData.tutorialClip;

        gameObject.SetActive(true);
    }

    private void HideInfo()
    {
        gameObject.SetActive(false);
    }
}
