using UnityEngine;

public class Item : IUsable
{
    private ItemData data;

    public int ID { get; set; }

    public Item(ItemData data)
    {
        this.data = data;

        ID = GenerateUniqueID();
    }

    public void Use(Player player)
    {
        if (data != null && data.effect != null)
        {
            Debug.Log($"Usando item: {data.itemName}");
            data.effect.ApplyEffect(player);
        }
        else
        {
            Debug.LogWarning("No hay ítem asignado o no tiene un efecto válido.");
        }
    }

    public void Remove(ItemModel model)
    {
        if (data == null) return;
        model.Data = null;
    }

    private int GenerateUniqueID()
    {
        return System.Guid.NewGuid().GetHashCode(); // Genera un hash único
    }
}
