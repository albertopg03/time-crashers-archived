using System;
using UnityEngine;

public class ItemSpace : Space
{
    public static event Action<Player, int> OnPlayerLandedOnItem;

    private readonly int currentIndexRoulette = 1;

    public override void OnPlayerLands(Player player)
    {
        base.OnPlayerLands(player);

        OnPlayerLandedOnItem?.Invoke(player, currentIndexRoulette);
    }
}
