using UnityEngine;

/// <summary>
/// Clase encargada de gestionar el estado que se ejecuta cuando el jugador cae en una casilla
/// </summary>
public class InSpaceState : IState
{
    private Player _player;
    private int _walk = Animator.StringToHash("Idle");

    /// <summary>
    /// Clase constructora al que se le pasa el contexto, que en este caso, sera el jugador
    /// </summary>
    /// <param name="player"></param>
    public InSpaceState(Player player)
    {
        _player = player;
    }

    public void Enter()
    {
        Debug.Log("COMIENZA ANIMACIÓN AL CAER EN UNA CASILLA X");
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
