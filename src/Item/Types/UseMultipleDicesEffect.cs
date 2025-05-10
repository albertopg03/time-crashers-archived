using UnityEngine;

/// <summary>
/// Tipo de efecto que permite al jugador tirar dos dados (dado que tenga en mano en este ejemplo).
/// Todo efecto deber� implementar el scriptable object Item Effect, el cual proporciona la funci�n
/// para ejecutar lo que queremos que ese efecto provoque
/// </summary>
[CreateAssetMenu(menuName = "ItemEffect/Use Two Dice")]
public class UseMultipleDicesEffect : ItemEffect
{
    public override void ApplyEffect(Player player)
    {
        int roll1 = player.dice.RollDice();
        int roll2 = player.dice.RollDice();
        int total = roll1 + roll2;

        Debug.Log($"El jugador us� dos dados y sac�: {roll1} y {roll2} (Total: {total})");
        player.board.Move(player, total);
    }
}
