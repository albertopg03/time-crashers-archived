using UnityEngine;
using System;

public class ItemModel : MonoBehaviour
{
    [SerializeField] private ItemData data;

    public event Action OnDataChanged; 

    public ItemData Data
    {
        get => data;
        set
        {
            data = value;
            OnDataChanged?.Invoke();
        }
    }
}
