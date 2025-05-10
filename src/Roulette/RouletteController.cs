using System.Collections.Generic;
using UnityEngine;
using Board;

public class RouletteController : MonoBehaviour
{
    [SerializeField] private TurnController turn;

    [HideInInspector] public List<EffectOptionRoulete> effectOptions;
    [SerializeField] private BoardController board;

    private int randomIndexSelected;

    // referencias
    [SerializeField] private RouletteUI ui;
    [SerializeField] private RouletteManager rouletteManager;

    private void OnEnable()
    {
        effectOptions = rouletteManager.rouletteOptionsCore[rouletteManager.currentIndexPosition].effectOptions;
        
        ui.OnFinishRoulete += ApplyRandomOption;

        AudioManager.Instance.SetParameterByName(AudioManager.Instance.MusicEventInstance, FMODEvents.instance.parameterData.parameterValueChanges[0].parameterName,
            FMODEvents.instance.parameterData.parameterValueChanges[0].inEventValue);
        // incializaci�n
        ChooseRandomOption();
    }

    private void OnDisable()
    {
        AudioManager.Instance.SetParameterByName(AudioManager.Instance.MusicEventInstance, FMODEvents.instance.parameterData.parameterValueChanges[0].parameterName,
            FMODEvents.instance.parameterData.parameterValueChanges[0].normalValue);
        ui.OnFinishRoulete -= ApplyRandomOption;
    }

    public void ChooseRandomOption()
    {
        randomIndexSelected = Random.Range(0, effectOptions.Count);
    }

    private void ApplyRandomOption()
    {
        EffectOptionRoulete optionSelected = effectOptions[randomIndexSelected];

        optionSelected.ApplyEffect(board.GetCurrentPlayer());

        Debug.Log("CAMBIA DE TURNO Y APLICA EVENTO");
        turn.ChangeTurn();
    }

    public int GetIndexOptionSelected()
    {
        return randomIndexSelected;
    }
}
