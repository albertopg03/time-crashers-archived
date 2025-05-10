using UnityEngine;

/// <summary>
/// Clase para la lógica de la casilla de intercambio
/// </summary>
public class ChangeSpace : Space
{
    public override void OnPlayerLands(Player player)
    {
        base.OnPlayerLands(player);

        Debug.Log("Ejecuto evento de casilla " + data.nameSpace);
    }
}
