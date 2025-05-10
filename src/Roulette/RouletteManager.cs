using UnityEngine;
using System;
using System.Collections.Generic;

public class RouletteManager : MonoBehaviour
{
    [Header("CORE ROULETTE")]
    [Space(10)]
    [SerializeField] private GameObject rouletteUIPanel; // Referencia a la UI de la ruleta

    [Header("OPTIONS LISTS")]
    [Space(10)]
    public List<RouletteManagerOption> rouletteOptionsCore;

    public int currentIndexPosition;

    public List<GameObject> gridOptions;

    public event Action OnRouletteManagerReady;

    [Serializable]
    public class RouletteManagerOption
    {
        public List<RouletteOptionData> dataList;
        public List<OptionModel> optionList;
        public List<EffectOptionRoulete> effectOptions;
    }

    private void OnEnable()
    {
        // Suscribirse al evento cuando el jugador cae en una casilla de fragmentos
        FragmentSpace.OnPlayerLandedOnFragment += ActivateRouletteUI;
        ItemSpace.OnPlayerLandedOnItem += ActivateRouletteUI;
        MinigameSpace.OnPlayerLandedOnMinigame += ActivateRouletteUI;
    }

    private void OnDisable()
    {
        // Desuscribirse del evento para evitar memory leaks
        FragmentSpace.OnPlayerLandedOnFragment -= ActivateRouletteUI;
        ItemSpace.OnPlayerLandedOnItem -= ActivateRouletteUI;
        MinigameSpace.OnPlayerLandedOnMinigame -= ActivateRouletteUI;
    }

    private void ActivateRouletteUI(Player player, int currentIndexPosition)
    {
        SetCurrentIndexPosition(currentIndexPosition);

        ActivateCurrentGrid(currentIndexPosition);

        rouletteUIPanel.SetActive(true); 

        OnRouletteManagerReady?.Invoke();
    }

    public void SetCurrentIndexPosition(int currentIndexPosition)
    {
        this.currentIndexPosition = currentIndexPosition;
    }

    private void ActivateCurrentGrid(int currentIndexPosition)
    {
        foreach(GameObject grid in gridOptions)
        {
            grid.SetActive(false);
        }

        gridOptions[currentIndexPosition].SetActive(true);
    }
}
