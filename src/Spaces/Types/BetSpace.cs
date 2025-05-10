using UnityEngine;

/// <summary>
/// Clase para la lógica de la casilla de apuesta
/// </summary>
public class BetSpace : Space
{
    public override void OnPlayerLands(Player player)
    {
        base.OnPlayerLands(player);

        Debug.Log("Ejecuto evento de casilla " + data.nameSpace);
    }
}
