using UnityEngine;

/// <summary>
/// Clase concreta que define la estructura base de un dado: generar un numero entre dos valores (segun el tipo de dado)
/// y devolver dicho numero. Ese numero lo recibira el controlador del tablero para saber cuantas casillas podra
/// moverse el jugador con turno actual
/// </summary>
public class Dice : MonoBehaviour
{
    // datos del dado. Aqui se settea el tipo de dado que queremos
    [SerializeField] private DiceData data;

    /// <summary>
    /// Funcion encargada de devolver un numero aleatorio entre dos valores setteados en los SO. Habra un SO por
    /// cada tipo de dado.
    /// </summary>
    /// <returns></returns>
    public int RollDice()
    {
        int roll = Random.Range(data.minValue, data.maxValue);

        return roll;
    }
}
