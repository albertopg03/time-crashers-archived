using System;
using UnityEngine;
using FMODUnity;
public class FMODEvents : MonoBehaviour
{
    public static FMODEvents instance;

    public FMODEventData eventData;        
    public FMODParameterData parameterData; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
