using UnityEngine;
using TMPro;
using Board;

public class ShopUI : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField] private BoardController board;

    [Header("UI")]
    [SerializeField] private TMP_Text remainingCyclesText;

    private ShopController shopController;


    private void Awake()
    {
        shopController = GetComponent<ShopController>();
    }

    private void OnEnable()
    {
        board.OnRemainingCycles += UpdateRemainingCycles;
    }

    private void UpdateRemainingCycles(int remainingCycles)
    {
        remainingCyclesText.text = remainingCycles.ToString();
    }
}
