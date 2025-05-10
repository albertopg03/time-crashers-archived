using UnityEngine;

/// <summary>
/// Clase encargada de gestionar el estado que se ejecuta cuando el jugador esta en espera de que se haga algo con el, ya sea tirar un dado o que sea su turno
/// </summary>
public class WaitingState : IState
{
    private Player _player;
    
    private static readonly int Idle = Animator.StringToHash("Idle");

    /// <summary>
    /// Clase constructora al que se le pasa el contexto, que en este caso, sera el jugador
    /// </summary>
    /// <param name="player"></param>
    public WaitingState(Player player)
    {
        this._player = player;
    }

    public void Enter()
    {
        //Debug.Log("Jugador entra en espera");
    }

    public void Update()
    {
        //Debug.Log("Jugador esperando su turno...");
    }

    public void Exit()
    {
        //Debug.Log("Jugador deja de esperar!");
    }
}