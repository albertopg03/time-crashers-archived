using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase para la l�gica de la casilla de elecci�n de ruta
/// </summary>
public class PathChoiceSpace : Space
{

    public List<Space> ChoiceOptions;

    public override void OnPlayerLands(Player player)
    {
        base.OnPlayerLands(player);

        Debug.Log("Ejecuto evento de casilla " + data.nameSpace);
    }
}
