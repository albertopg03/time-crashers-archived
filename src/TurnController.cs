using Board;
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Clase encargada de controlar el flujo de turnos entre los jugadores instanciados en la partida
/// </summary>
public class TurnController : MonoBehaviour
{
    [Header("BOARD")]
    [Space(10)]
    [SerializeField] private BoardController boardController;

    [Header("BUTTONS")]
    [Space(10)]
    [SerializeField] private Button playTurnBtn;
    [SerializeField] private Button nextTurnBtn;

    // actions
    public event Action OnChangeTurn; // finaliza el turno del jugador actual y cambia de jugador
    public event Action OnStartTurn; // comienza el turno (a moverse concretamente) del jugador actual

    private void Start()
    {
        playTurnBtn.onClick.AddListener(OnPlayTurnClicked);
        //nextTurnBtn.onClick.AddListener(OnNextTurnClicked);
    }

    private void OnDestroy()
    {
        playTurnBtn.onClick.RemoveListener(OnPlayTurnClicked);
        //nextTurnBtn.onClick.RemoveListener(OnNextTurnClicked);
    }

    /// <summary>
    /// Funci�n encargada de ejecutarse en el listener del bot�n de play, cuando el jugador actual solicite
    /// lanzar un dado para poder moverse
    /// </summary>
    /// 
    private void OnPlayTurnClicked()
    {
        Player currentPlayer = boardController.GetCurrentPlayer();
        currentPlayer.TakeMove();
        OnStartTurn?.Invoke();
    }
    
    /// <summary>
    /// Funci�n encargada de ejecutarse en el listener del bot�n de next turn, cuando el jugador actual solicite
    /// cambiar de turno
    /// </summary>
    /// 
    /*
    private void OnNextTurnClicked()
    {
        boardController.NextPlayer();
    }
    */

    public void ChangeTurn()
    {
        OnChangeTurn?.Invoke();
        boardController.NextPlayer();
    }
}
