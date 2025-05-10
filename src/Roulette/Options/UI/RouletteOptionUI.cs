using TMPro;
using UnityEngine;
using System.Collections;

public abstract class RouletteOptionUI : MonoBehaviour
{
    [Header("DATA")]
    [Space(5)]
    [SerializeField] protected OptionModel model;

    [Header("UI REFERENCES")]
    [Space(5)]
    [SerializeField] protected TMP_Text description;

    protected abstract void UpdateUI();

    private void Awake()
    {
        model = GetComponent<OptionModel>();
        InitializeEffect();
    }

    private void OnEnable()
    {
        StartCoroutine(DelayedUIUpdate());
    }

    private IEnumerator DelayedUIUpdate()
    {
        yield return new WaitForSeconds(0.6f); // Asegura que GainFragmentsEffect haya inicializado primero
        UpdateUI();
    }

    protected abstract void InitializeEffect();
}
