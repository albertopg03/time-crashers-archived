using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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

    private void Start()
    {
        //TRAEMOS y SETEAMOS EL LIMITE DEL FPS
        Application.targetFrameRate = PlayerPrefs.GetInt("FPSLimit");
        
        //TRAEMOS y SETEAMOS EL QUALITY SETTINGS
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("GraphicsQuality"));
    }
}
