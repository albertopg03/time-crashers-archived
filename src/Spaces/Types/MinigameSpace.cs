using System;
using UnityEngine;

public class MinigameSpace : Space
{
    public static event Action<Player, int> OnPlayerLandedOnMinigame;

    private readonly int currentIndexRoulette = 2;

    public override void OnPlayerLands(Player player)
    {
        Debug.Log("CAES EN CASILLA DE MINIJUEGO");

        base.OnPlayerLands(player);

        OnPlayerLandedOnMinigame?.Invoke(player, currentIndexRoulette);
    }
}
