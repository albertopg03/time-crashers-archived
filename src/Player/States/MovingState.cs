using UnityEngine;

/// <summary>
/// Clase encargada de gestionar el estado que se ejecuta cuando el jugador se esta moviendo por una ruta definida
/// </summary>
public class MovingState : IState
{
    private Player _player;
    private int _walk = Animator.StringToHash("Walk");

    /// <summary>
    /// Clase constructora al que se le pasa el contexto, que en este caso, sera el jugador
    /// </summary>
    /// <param name="player"></param>
    public MovingState(Player player)
    {
        this._player = player;
    }
    
    public void Enter()
    {
      //Debug.Log("Jugador comienza estado de movimiento");
    }

    public void Update()
    {
        //Debug.Log("Jugador se est� moviendo");
    }

    public void Exit()
    {
        //Debug.Log("Jugador termina de moverse");
    }
}
