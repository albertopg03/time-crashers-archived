using UnityEngine;

/// <summary>
/// ScriptableObject de los datos que contendr� cada uno de los items. Cuando se desee crear un nuevo item, 
/// este deber� tener datos, por lo que habr� que crear este scriptable object
/// 
/// itemName -> nombre del objeto
/// description -> descripci�n de lo que hace el objeto
/// icon -> imagen del icono
/// effect -> Scriptable object del tipo de efecto que har� este objeto
/// </summary>
[CreateAssetMenu(menuName = "Item/New Item", order = 1)]
public class ItemData : ScriptableObject
{
    public string itemName; 
    public string description; 
    public Sprite icon; 
    public ItemEffect effect; 
}
