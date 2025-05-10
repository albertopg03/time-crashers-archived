using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider))]
public class EventClick : MonoBehaviour, IPointerClickHandler
{
    public static event Action<int> OnSelectSpace;
    [SerializeField] private int index = 0;

    private BoxCollider bxCollider;

    private void Awake()
    {
        bxCollider = GetComponent<BoxCollider>();

        bxCollider.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OBJETO CLICADO_______________________");
        OnSelectSpace?.Invoke(index);
    }
}
