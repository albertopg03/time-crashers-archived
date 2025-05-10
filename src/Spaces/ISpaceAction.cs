using UnityEngine;

/// <summary>
/// Interfaz que permite implementar una función para ejecutar una acción al caer sobre una casilla
/// </summary>
public interface ISpaceAction
{
    void Execute(Player player, Space space);
}
