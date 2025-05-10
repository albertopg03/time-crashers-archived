using System;
using UnityEngine;
using Board;
using TMPro;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [Header("HUD ELEMENTS")]
    [Space(10)]
    [SerializeField] private GameObject HUD;
    [SerializeField] private Button shopBtn;

    [Header("SUBJECTS")]
    [Space(10)]
    [SerializeField] private BoardController board;
    
    public static HUDController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowHUD()
    {
        //HUD.SetActive(true);
    }

    public void HideHUD()
    {
        //HUD.SetActive(false);
    }
}
