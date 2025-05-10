using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase abstracta padre de la cual heredará todas las casillas con comportamiento comunes entre sí. Esta es la clase padre
/// </summary>
public abstract class Space : MonoBehaviour
{
    public SpaceData data; // datos de la casilla

    public ISpaceAction Action; // implementacion de la accion de la casilla

    public List<Space> SpaceOptions; // lista de casillas con las que esta casilla esta conectada con las demas

    /// <summary>
    /// Funcion que se ejecuta en el momento en el que un jugador caiga sobre una casilla
    /// 
    /// player->jugador al que aplicarle el efecto de la casilla en la que caiga
    /// </summary>
    /// <param name="player"></param>
    public virtual void OnPlayerLands(Player player)
    {
        Debug.Log("ON PLAYER LANDS DE ALGUNA CASILLA");
        Action?.Execute(player, this); 
    }
}
