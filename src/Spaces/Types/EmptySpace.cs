using UnityEngine;

/// <summary>
/// Clase para la l�gica de la casilla vac�a
/// </summary>
public class EmptySpace : Space
{
    public override void OnPlayerLands(Player player)
    {
        base.OnPlayerLands(player);

        Debug.Log("Ejecuto evento de casilla " + data.nameSpace);
    }
}
