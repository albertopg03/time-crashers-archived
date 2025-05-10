using Board;
using FMODUnity;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("BOARD REFERENCES")]
    [Space(10)]
    [SerializeField] public BoardController board;
    [Space(5)]
    public Dice dice;

    [Header("COMPONENTS")]
    public PlayerState state;
    public PlayerStats stats { get; set; }
    public PlayerAnimationData PlayerAnimationData;

    [Header("INVENTORY")]
    public PlayerInventory inventory { get; set; }


    // Índice de la casilla donde el jugador está
    [SerializeField] public int currentSpaceIndexPosition = 0;

    // Número de casillas restantes
    [SerializeField] public int nSpaceMoves;
    

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
        inventory = GetComponent<PlayerInventory>();
    }

    private void OnEnable()
    {
        board.OnPlayerMovement += state.MovingState;
        board.OnFinishPlayerMove += state.ResetState;
        board.OnPlayerRouteChoice += state.PathChoiceState;
        board.OnPlayerLandsInSpace += state.LandSpaceState;
    }

    private void OnDisable()
    {
        board.OnPlayerMovement -= state.MovingState;
        board.OnFinishPlayerMove -= state.ResetState;
        board.OnPlayerRouteChoice -= state.PathChoiceState;
        board.OnPlayerLandsInSpace -= state.LandSpaceState;
    }

    private void Start()
    {
        state.Init(this);
    }

    private void Update()
    {
        state?.Update();
    }

    /// <summary>
    /// Función encargada de reallizar una peticion de movimiento al tablero, para que el jugador pueda moverse
    /// </summary>
    public void TakeMove()
    {
        int roll = dice.RollDice();

        board.Move(this, roll);
    }
}
