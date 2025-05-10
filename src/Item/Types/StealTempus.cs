using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/Steal Tempus")]
public class StealTempus : ItemEffect
{
    public override void ApplyEffect(Player player)
    {
        Debug.Log("EJECUTO OBJETO ROBAR TEMPUS!!!");
        player.board.Move(player, 3);
    }
}
