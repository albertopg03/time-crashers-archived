using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnhancedSingleton<T> : MonoBehaviour where T : Component
{

    public static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                Debug.Log(FindObjectOfType<T>());
                if (_instance == null)
                {
                    GameObject obj = new GameObject("Auto-generated" + typeof(T));
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }
    
    
    
    
}
