using TMPro;
using UnityEngine;
using System.Collections;

public class FragmentOptionUI : RouletteOptionUI
{
    private IHasValue effect;

    protected override void InitializeEffect()
    {
        effect = GetComponent<IHasValue>();
    }

    protected override void UpdateUI()
    {
        Debug.Log("VALOR DE LA UI EN " + gameObject.name + " = " + effect.Value);
        description.text = model.data.GetTextData(effect.Value);
    }
}