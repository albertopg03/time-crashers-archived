using TMPro;
using UnityEngine;
using System.Collections;

public class ItemOptionUI : RouletteOptionUI
{
    private EffectOptionRoulete effect;

    protected override void InitializeEffect()
    {
        effect = GetComponent<EffectOptionRoulete>();
    }

    protected override void UpdateUI()
    {
        description.text = $"<size={(int)model.data.iconSize}%><sprite=\"{model.data.spriteAsset.name}\" index={model.data.indexImage}></size>";
    }
}
