using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Board
{
    /// <summary>
    /// Clase controladora de la UI
    /// </summary>
    public class BoardUI : MonoBehaviour
    {
        [Header("UI ELEMENTS")]
        [Space(10)]
        [SerializeField] private Button playTurnBtn; // Botón para tirar el dado
        [SerializeField] private TMP_Text turnText;

        [Header("OBJECT REFERENCES")]
        [Space(10)]
        [SerializeField] private BoardController board; // Referencia al controlador del tablero
        [SerializeField] private TurnController turn;    // Referencia al controlador de turnos

        private Player currentPlayer; // Referencia al jugador actual

        // Variable para rastrear cambios en el estado de evento del tablero.
        private bool lastBoardEventState;

        private void OnEnable()
        {
            // Inicializamos el estado previo del evento con el estado actual
            lastBoardEventState = board.inEvent;

            currentPlayer = board.GetCurrentPlayer();

            turn.OnStartTurn += UpdateTarget;
            
            currentPlayer.state.OnWaiting += ShowOptionsUI;
            currentPlayer.state.OnMoving += HideOptionsUI;

            board.OnTurnLoopReset += UpdateTurn;

            // Al comienzo de la partida, si no hay evento, se muestra el HUD.
            if (!board.inEvent)
            {
                HUDController.Instance?.ShowHUD();
            }
        }

        private void OnDisable()
        {
            
            turn.OnStartTurn -= UpdateTarget;

            board.OnTurnLoopReset -= UpdateTurn;


            // Desuscribir eventos del jugador actual.
            currentPlayer.state.OnWaiting -= ShowOptionsUI;
            currentPlayer.state.OnMoving -= HideOptionsUI;
                
            // Desuscribir la acción del botón si se había asignado.
            playTurnBtn.onClick.RemoveAllListeners();
            
        }

        /// <summary>
        /// Actualiza el jugador actual y suscriptores de eventos asociados.
        /// </summary>
        private void UpdateTarget()
        {
            // Desuscribir los eventos del jugador anterior.
            
            currentPlayer.state.OnWaiting -= ShowOptionsUI;
            currentPlayer.state.OnMoving -= HideOptionsUI;
            

            // Actualizar el jugador actual.
            currentPlayer = board.GetCurrentPlayer();

            // Suscribir los eventos para el nuevo jugador.
            currentPlayer.state.OnWaiting += ShowOptionsUI;
            currentPlayer.state.OnMoving += HideOptionsUI;
            
        }

        /// <summary>
        /// Muestra el HUD del tablero si no se está en un evento.
        /// </summary>
        private void ShowOptionsUI()
        {
            if (!board.inEvent)
            {
                //HUDController.Instance.ShowHUD();
            }
            
        }

        /// <summary>
        /// Oculta el HUD del tablero.
        /// </summary>
        private void HideOptionsUI()
        {
            //HUDController.Instance?.HideHUD();
        }

        private void UpdateTurn(int nRound)
        {
            turnText.text = nRound.ToString();
        }

        /// <summary>
        /// Monitorea el estado del evento en el tablero para actualizar el HUD cuando el evento termina o comienza.
        /// </summary>
        private void Update()
        {
            // Si hay cambio en el estado del evento...
            if (board.inEvent != lastBoardEventState)
            {
                // Si acaba de terminar el evento, y el jugador debería ver el HUD (por ejemplo, cuando esté esperando)
                if (!board.inEvent)
                {
                    // Aquí podemos verificar también el estado del jugador si es necesario.
                    ShowOptionsUI();
                }
                else
                {
                    // Si se inicia un evento, se oculta el HUD.
                    HideOptionsUI();
                }
                lastBoardEventState = board.inEvent;
            }
        }
    }
}

