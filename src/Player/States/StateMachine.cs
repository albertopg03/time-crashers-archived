using UnityEngine;

/// <summary>
/// Clase encargada de controlar los estados de los objetos que vayan a implementar su propia maquina de estados
/// </summary>
public class StateMachine
{
    // referencia al estado actual del objeto
    private IState currentState;

    /// <summary>
    /// Funcion encargada de establecer un nuevo estado
    /// 
    /// newState-> siguiente estado del contexto
    /// </summary>
    /// <param name="newState"></param>
    public void SetState(IState newState)
    {
        currentState?.Exit();

        currentState = newState;

        currentState?.Enter();
    }

    /// <summary>
    /// Función encargada de ejecutar en cada frame el estado actual que tenga la maquina de estados
    /// </summary>
    public void Update()
    {
        currentState?.Update();
    }
}
