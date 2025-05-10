using UnityEngine;

/// <summary>
/// Scriptable objet que nos permite crear diferentes tipos de datos. Creamos uno por cada dado, 
/// asignamos los datos que queramos ya luego lo usará el dado en cuestión
/// </summary>
[CreateAssetMenu(menuName = "Dice/Type", order = 1)]
public class DiceData : ScriptableObject
{
    public string nameDice;

    public int minValue = 1;
    public int maxValue = 2;
}
