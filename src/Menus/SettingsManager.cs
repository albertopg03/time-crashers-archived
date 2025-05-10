using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("MUSIC SETTINGS")]
    [Space(10)]
    public Slider musicVolumeSlider;
    public Slider ambientVolumeSlider;
    public Slider sfxVolumeSlider;
    
    [Header("OTHER SETTINGS")]
    [Space(10)]
    public Button qualityButton;
    public TMP_Text qualityText;
    public Button FPSButton;
    public TMP_Text FPSText;
    public Toggle postProcessingToggle;
    
    
    public static SettingsManager Instance;

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
        //ACTUALIZAMOS LA UI DEL MENU DE OPCIONES
        LoadSettings();
    }
    
    
    #region SETTERS
    public void SetSFXVolume()
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
    }

    public void SetAmbientVolume()
    {
        PlayerPrefs.SetFloat("AmbientVolume", ambientVolumeSlider.value);
    }
    
    public void SetMusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
    }

    public void SetGraphicsQuality()
    {
        //INDICE ACTUAL DEL QUALITY SETTING DEFINIDO
        int currentQuality = QualitySettings.GetQualityLevel();
    
        //TOTAL DE NIVELES DEFINIDOS EN EL PROYECTO
        int totalQualityLevels = QualitySettings.names.Length;
    
        //SE CALCULA EL SIGUIENTE NIVEL DE CALIDAD Y SI ES EL MAXIMO SE VUELVE AL PRIMERO
        int nextQuality = (currentQuality + 1) % totalQualityLevels;
    
        //SE APLICA LA CALIDAD
        QualitySettings.SetQualityLevel(nextQuality);
    
        //GUARDAMOS EN LOS PLAYER PREFS
        PlayerPrefs.SetInt("GraphicsQuality", nextQuality);

        //ACTUALIZACIOND DEL TEXTO RESPECTIVO A LA CALIDAD 
        if (nextQuality == 0)
        {
            qualityText.text = "LOW QUALITY";

        }
        else if (nextQuality == 1)
        {
            qualityText.text = "MEIDUM QUALITY";
        }
        else
        {
            qualityText.text = "HIGH QUALITY";
        }
        
    }

    public void SetFPSLimit()
    {
        //SOLO HAY DOS FPS LIMIT DISPONIBLE, 30 Y 60
        if (PlayerPrefs.GetInt("FPSLimit") == 30)
        {
            //SE GUARDA EN EL PLAYER PREF EL NUEVO LIMITE
            PlayerPrefs.SetInt("FPSLimit", 60);
            
            //SE APLICA EL TARGET FRAME RATE CORRESPONDIENTE
            Application.targetFrameRate = 60;
            
            //SE ACTUALIZA EL TEXTO RESPECTVO AL LIMITE DE FPS
            FPSText.text = PlayerPrefs.GetInt("FPSLimit") + " FPS";
        }
        else
        {
            //SE GUARDA EN EL PLAYER PREF EL NUEVO LIMITE
            PlayerPrefs.SetInt("FPSLimit", 30);
            
            //SE APLICA EL TARGET FRAME RATE CORRESPONDIENTE
            Application.targetFrameRate = 30;
            
            //SE ACTUALIZA EL TEXTO RESPECTVO AL LIMITE DE FPS
            FPSText.text = PlayerPrefs.GetInt("FPSLimit") + " FPS";
        }
    }

    public void SetPostProcessing()
    {
        PlayerPrefs.SetInt("PostProcessing", postProcessingToggle.isOn ? 1 : 0);
    }
    #endregion
    
    #region GETTERS
    
    public float GetSFXVolume()
    {
       float sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
       return sfxVolume;
    }

    public float GetAmbientVolume()
    {
        float ambientVolume = PlayerPrefs.GetFloat("AmbientVolume");
        return ambientVolume;
    }
    
    public float GetMusicVolume()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        return musicVolume;
    }
    
    public int GetFPSLimit()
    {
        int fpsLimit = PlayerPrefs.GetInt("FPSLimit");

        return fpsLimit;
    }
    
    public int GetGraphicsQuality()
    {
        
        int qualitySetting =  PlayerPrefs.GetInt("GraphicsQuality");
        return qualitySetting;
    }
    
    #endregion
    public void LoadSettings()
    {
        
        //MUSIC AND SOUNDS SLIDERS UPDATE
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        ambientVolumeSlider.value = PlayerPrefs.GetFloat("AmbientVolume");
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        
        //QUALITY SETTINGS TEXT UPDATE
        if (PlayerPrefs.GetInt("GraphicsQuality") == 0)
        {
            qualityText.text = "LOW QUALITY";
        }
        else if (PlayerPrefs.GetInt("GraphicsQuality") == 1)
        {
            qualityText.text = "MEDIUM QUALITY";
        }
        else if(PlayerPrefs.GetInt("GraphicsQuality") == 2)
        {
            qualityText.text = "HIGH QUALITY";
        }
        
        //FPS LIMIT TEXT UPDATE
        FPSText.text = PlayerPrefs.GetInt("FPSLimit") + " FPS";
        
        //POST PROCESSING TOGGLE UPDATE
        postProcessingToggle.isOn = PlayerPrefs.GetInt("PostProcessing") == 1;
    }
}
