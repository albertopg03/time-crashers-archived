using UnityEngine;

/// <summary>
/// Clase para la lógica de la casilla de evento
/// </summary>
public class EventSpace : Space
{
    public override void OnPlayerLands(Player player)
    {
        base.OnPlayerLands(player);

        Debug.Log("Ejecuto evento de casilla " + data.nameSpace);
    }
}
