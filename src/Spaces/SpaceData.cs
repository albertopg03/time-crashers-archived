using UnityEngine;

/// <summary>
/// Scriptable Object que permitira crear variantes de casillas, asignandole sus propias caracterisitcas, como nombre o tipo
/// </summary>
[CreateAssetMenu(menuName = "Board/SpaceData")]
public class SpaceData : ScriptableObject
{
    // tipo de casilla core. Por el momento solo habra dos casillas:
    // casilla Normal: esta casilla solo tendra dos direcciones (alante o atras) que no se podra escoger entre ellas y que ejecuta un evento al caer sobre ella
    // casilla Choice: esta casilla tendra varias direcciones, y permitira al jugador elegir entre una de ellas. Para el jugador, esto no es una casilla persé
    // para que se entienda mejor, se podría decir que TypeSpace actúa como una etiqueta. Todos son casillas pero dentro de eso, una tiene una etiqueta y otra tiene otra etiqueta
    public enum TypeSpace
    {
        Normal,
        Choice,
    }

    // Selector de los tipos de casillas core, en el Inspector
    public TypeSpace typeSpace;

    public string nameSpace;
}
