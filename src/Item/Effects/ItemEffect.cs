using UnityEngine;

/// <summary>
/// Scriptable object que permite implementar un efecto a un objeto. Al usarlo en un item,
/// permitirá que este pueda ejecutar un uso sobre el jugador.
/// </summary>
public abstract class ItemEffect : ScriptableObject
{
    /// <summary>
    /// Funcion encargada de aplicar un effecto en concreto segun el tipo de objeto. Esta funcion
    /// se ejecutara en el momento en el que se caiga sobre esta casilla
    /// </summary>
    /// <param name="player"></param>
    public abstract void ApplyEffect(Player player);
}

