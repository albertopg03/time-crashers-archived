using System;
using UnityEngine;

/// <summary>
/// Clase encargada de controlar todos los estados del jugador, ejecutar un estado y omitir otro...
/// </summary>
public class PlayerState : MonoBehaviour
{
    // referencia al jugador 
    private Player _player;

    // objetos de todas los estados posibles del jugador
    private WaitingState waitingState;
    private MovingState movingState;
    private PathChoiceState pathChoiceState;
    private InSpaceState inSpaceState;

    // maquina de estados (controlador de estados interno) del jugador
    private StateMachine stateMachine;

    // diferentes eventos posibles en los que podra estar el jugador
    public event Action OnWaiting;
    public event Action OnMoving;
    public event Action OnPathChoice;
    public event Action OnSpace;

    public bool CanBuy { get; set; }

    /// <summary>
    /// Funcion inicializadora para resetear valores
    /// </summary>
    /// <param name="player"></param>
    public void Init(Player player)
    {
        _player = player;

        CanBuy = true;

        InitStates();

        stateMachine = new StateMachine();
        stateMachine?.SetState(waitingState);
        OnWaiting?.Invoke();
    }

    /// <summary>
    /// Funci�n encargada de inicializar todos los objetos estado posibles del jugador
    /// </summary>
    private void InitStates()
    {
        waitingState = new WaitingState(_player);
        movingState = new MovingState(_player);
        pathChoiceState = new PathChoiceState(_player);
        inSpaceState = new InSpaceState(_player);
    }

    /// <summary>
    /// Funci�n para activar el estado de movimiento del jugador
    /// </summary>
    /// <param name="caller"></param>
    public void MovingState(Player caller)
    {
        if (caller != _player) return;

        CanBuy = true;
        stateMachine?.SetState(movingState);
        OnMoving?.Invoke();
    }

    /// <summary>
    /// Funci�n para activar el estado de estar en espera del jugador
    /// </summary>
    /// <param name="caller"></param>
    public void ResetState(Player caller)
    {
        if (caller != _player) return;

        stateMachine?.SetState(waitingState);
        OnWaiting?.Invoke();
    }

    /// <summary>
    /// Funcion para activar el estado de eleccion de ruta del jugador
    /// </summary>
    /// <param name="caller"></param>
    public void PathChoiceState(Player caller)
    {
        if (caller != _player) return;

        Debug.Log("AAAAAAAAAAAAAAACTIVO ELECCION DE RUTA!!!!!!!!!!!!!");

        stateMachine?.SetState(pathChoiceState);
        OnPathChoice?.Invoke();
    }

    /// <summary>
    /// Funcion para activar el estado de cuando el jugador cae en una casilla nueva
    /// </summary>
    /// <param name="caller"></param>
    public void LandSpaceState(Player caller)
    {
        if (caller != _player) return;

        stateMachine?.SetState(inSpaceState);
        OnSpace?.Invoke();
    }

    public void Update()
    {
        stateMachine?.Update();
    }
}
