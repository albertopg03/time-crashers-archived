using UnityEngine;

public interface IState
{
    /// <summary>
    /// Funcion que se ejecuta en el primer frame, es decir, en el primer momento en el que entra el nuevo estado
    /// </summary>
    void Enter();

    /// <summary>
    /// Funcion que se ejecuta una vez por cada frame, es decir, durante el estado
    /// </summary>
    void Update();

    /// <summary>
    /// Función que se ejecuta justo antes de cambiar de estado
    /// </summary>
    void Exit();
}
