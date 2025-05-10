using UnityEngine;

/// <summary>
/// Clase para la lógica de la casilla vacía
/// </summary>
public class EmptySpace : Space
{
    public override void OnPlayerLands(Player player)
    {
        base.OnPlayerLands(player);

        Debug.Log("Ejecuto evento de casilla " + data.nameSpace);
    }
}
