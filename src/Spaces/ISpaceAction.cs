using UnityEngine;

/// <summary>
/// Interfaz que permite implementar una funci�n para ejecutar una acci�n al caer sobre una casilla
/// </summary>
public interface ISpaceAction
{
    void Execute(Player player, Space space);
}
