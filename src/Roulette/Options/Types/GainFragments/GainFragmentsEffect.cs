using UnityEngine;
using System.Collections;

public class GainFragmentsEffect : EffectOptionRoulete, IHasValue
{
    [Header("DATA")]
    [Space(5)]
    public OptionModel model;

    private int value;
    public int Value => value;

    private void Awake()
    {
        model = GetComponent<OptionModel>();
    }

    private void OnEnable()
    {
        PopulationOptions.OnOptionsUpdated += InitializeValue;

        // Llamamos directamente a InitializeValue() sin esperar
        StartCoroutine(DelayedInitializeValue());
    }


    private void OnDisable()
    {
        PopulationOptions.OnOptionsUpdated -= InitializeValue;
    }

    private IEnumerator DelayedInitializeValue()
    {
        yield return new WaitForSeconds(0.5f);
        InitializeValue();
    }

    private void InitializeValue()
    {
        value = GetRandomMultipleOfFive(model.data.minValue, model.data.maxValue);
        Debug.Log("VALOR GENERADO EN " + gameObject.name + " = " + value);
    }

    private int GetRandomMultipleOfFive(int min, int max)
    {
        if (min > max)
        {
            (min, max) = (max, min);
        }

        min = Mathf.CeilToInt(min / 5.0f) * 5;
        max = Mathf.FloorToInt(max / 5.0f) * 5;

        if (min > max) return min;

        int count = (max - min) / 5 + 1;
        return min + (Random.Range(0, count) * 5);
    }

    public override void ApplyEffect(Player player)
    {
        Debug.Log("Aplico efecto de la opción-> " + gameObject.name + " sobre jugador: " + player.gameObject.name + " que recibirá " + Value + " fragmentos");
        player.stats.Fragments += Value;
    }
}
