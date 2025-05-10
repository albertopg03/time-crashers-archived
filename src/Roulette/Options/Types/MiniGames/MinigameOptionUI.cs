using UnityEngine;

public class MinigameOptionUI : RouletteOptionUI
{
    private IMinigame effect;

    protected override void InitializeEffect()
    {
        effect = GetComponent<IMinigame>();
    }

    protected override void UpdateUI()
    {
        description.text = effect.Minigame.name; 
    }
}
