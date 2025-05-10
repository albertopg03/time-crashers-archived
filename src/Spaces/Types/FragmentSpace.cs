using System;
using UnityEngine;

/// <summary>
/// Clase para la lógica de la casilla de fragmentos
/// </summary>
public class FragmentSpace : Space
{

    public static event Action<Player, int> OnPlayerLandedOnFragment;

    private readonly int currentIndexRoulette = 0;

    public override void OnPlayerLands(Player player)
    {
        base.OnPlayerLands(player);

        OnPlayerLandedOnFragment?.Invoke(player, currentIndexRoulette);
    }
}
