using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Board
{
    /// <summary>
    /// Clase encargada de controlar todo lo que pasa en el tablero, principalmente, 
    /// el movimiento de los jugadores y su situacion en el tablero.
    /// </summary>
    public class BoardController : MonoBehaviour
    {
        // referencia al controlador de turnos
        [SerializeField] private TurnController turn;

        [Header("SPACES")]
        [Space(10)]
        [SerializeField] private GameObject mInitialSpace;
        [SerializeField] private float timeBySpace;
        [SerializeField] private float offsetBySpace = 2.5f;

        [Header("PLAYERS")]
        [Space(10)]
        [SerializeField] private List<Player> mPlayers = new List<Player>();
        [SerializeField] private List<GameObject> mSpaceObjects = new List<GameObject>();

        [Header("EVENTS")]
        [Space(10)]
        [SerializeField] private RouletteUI rouletteUI;

        public readonly Dictionary<Vector3, Space> GSpaceCollection = new Dictionary<Vector3, Space>();

        // eventos globales
        public event Action OnStartGame;

        // eventos de movimiento
        public event Action OnStartTurn;
        public event Action<Player> OnFinishPlayerMove;
        public event Action<Player> OnPlayerMovement;

        // eventos al caer en una casilla normal
        public event Action<Player> OnPlayerLandsInSpace;

        // eventos al caer en la casilla de elección de ruta
        public event Action<Player> OnPlayerRouteChoice;

        // eventos de los eventos de ciertas casillas
        public event Action OnEnterEvent;
        public event Action OnExitEvent;

        // evento al resetearse el loop de turnos
        public event Action<int> OnTurnLoopReset;
        public event Action OnRotationTurn;
        public event Action<int> OnRemainingCycles;
        public event Action OnEventShop;

        // índice del mapa para saber en que casilla esta el jugador
        private byte _mCurrentPlayerIndex = 0;

        // lista de nodos para saber la ruta del jugador
        [Header("LISTAS DE CASILLAS")]
        [Space(10)]
        private List<Vector3> mPath;
        private List<Space> mPathSpaces;

        // variables para la secuencia
        private Sequence sequence;
        private int indexWayPoint = 0;

        public bool inEvent = false;

        // luego quitarlo del inspector
        private int nCycle = 0; // número de ciclo (+1 tras moverse todos los jugadores)
        private int nMovement = 0; // número de movimiento (aumenta uno por cada movimiento de un jugador hasta el final de la partida)
        private int remainingCycles = 3;// número de ciclos restantes

        //TODO: QUITAR ESTO LUEGO
        private int turnBoardNumber;

        private void OnEnable()
        {
            rouletteUI.OnStartRoulete += StartEvent;
            rouletteUI.OnFinishRoulete += FinishEvent;
        }

        private void OnDisable()
        {
            rouletteUI.OnStartRoulete -= StartEvent;
            rouletteUI.OnFinishRoulete -= FinishEvent;
        }

        private void Awake()
        {
            _mCurrentPlayerIndex = 0;

            InitBoard();
        }

        private void Start()
        {
            OnStartGame?.Invoke();
            OnStartTurn?.Invoke();

            InitPlayerPositions();

            //TODO QUITAR LUEGO ESTO
            turnBoardNumber = 0;

            nCycle = 0; // empezar en el ciclo 0
            nMovement = 1; // empezar en la ronda de movimiento 1

            AudioManager.Instance.SetParameterByLabel(AudioManager.Instance.MusicEventInstance, FMODEvents.instance.parameterData.parameterLabelChanges[0].parameterName,
                FMODEvents.instance.parameterData.parameterLabelChanges[0].normalLabel);
        }

        private void Update()
        {

            //TODO: LUEGO QUITAR ESTO, ES SOLO UNA PRUEBA, CADA VEZ QUE LE DAMOS A LA U, ES COMO SI EL JUGADOR DIERA UNA VUELTA COMPLETA AL TABLERO
            if (Input.GetKeyDown(KeyCode.U))
            {
                if (turnBoardNumber > FMODEvents.instance.parameterData.parameterLabelChanges.Length)
                {
                    Debug.Log("Pasaste a la primera vuelta otravez");
                    turnBoardNumber = 0;
                    AudioManager.Instance.SetParameterByLabel(AudioManager.Instance.MusicEventInstance, FMODEvents.instance.parameterData.parameterLabelChanges[0].parameterName,
                        FMODEvents.instance.parameterData.parameterLabelChanges[0].normalLabel);
                }
                else
                {
                    Debug.Log("Pasaste " + (turnBoardNumber + 1) + " vuelta");
                    AudioManager.Instance.SetParameterByLabel(AudioManager.Instance.MusicEventInstance, FMODEvents.instance.parameterData.parameterLabelChanges[0].parameterName,
                        FMODEvents.instance.parameterData.parameterLabelChanges[0].inEventLabel[turnBoardNumber]);
                    turnBoardNumber++;
                }


            }
        }

        #region INITIALIZATION
        private void InitBoard()
        {
            GSpaceCollection.Clear();
            foreach (GameObject space in mSpaceObjects)
            {
                Space spaceComponent = space.GetComponent<Space>();
                Vector3 position = space.transform.GetChild(0).position;

                GSpaceCollection.TryAdd(position, spaceComponent);
            }
        }

        private void InitPlayerPositions()
        {
            foreach (Player player in mPlayers)
            {
                player.transform.position = mInitialSpace.transform.GetChild(0).position;
                player.currentSpaceIndexPosition = 0;
            }
        }
        #endregion

        #region PLAYER UTILITIES
        public Player GetCurrentPlayer()
        {
            return mPlayers[_mCurrentPlayerIndex];
        }

        public byte GetCurrentPlayerIndex()
        {
            return _mCurrentPlayerIndex;
        }

        public byte GetNextPlayerIndex()
        {
            byte nextPlayerIndex = (byte)((_mCurrentPlayerIndex + 1) % mPlayers.Count);
            return nextPlayerIndex;
        }

        public byte GetCountPlayers()
        {
            return (byte)mPlayers.Count;
        }

        public Player GetPlayer(int index)
        {
            return mPlayers[index];
        }

        public Player GetNextPlayer()
        {
            return GetPlayer(GetNextPlayerIndex());
        }

        public void NextPlayer()
        {
            _mCurrentPlayerIndex = (byte)((_mCurrentPlayerIndex + 1) % mPlayers.Count);

            CheckTurnLoopReset();
        }

        private void CheckTurnLoopReset()
        {
            if (_mCurrentPlayerIndex == 0 && nMovement > 0)
            {
                nCycle++;
                remainingCycles--;

                OnTurnLoopReset?.Invoke(nCycle);
                OnRemainingCycles?.Invoke(remainingCycles);

                if (nCycle % 3 == 0 && nCycle != 0)
                {
                    OnRotationTurn?.Invoke();
                    OnRemainingCycles?.Invoke(3);

                    remainingCycles = 3;
                }

                if (nCycle % 6 == 0)
                {
                    OnEventShop?.Invoke();
                }
            }

            nMovement++;
        }


        public int GetCurrentRound()
        {
            return nCycle;
        }

        #endregion

        #region MOVEMENT
        public void Move(Player player, int spaces)
        {
            // inicializamos el número de casillas que se moverá el jugador
            player.nSpaceMoves = spaces;

            // inicializamos a vacío las rutas
            mPath = new List<Vector3>();
            mPathSpaces = new List<Space>();

            // casilla inicial y siguiente
            Space initialSpace = GSpaceCollection.Values.ElementAt(player.currentSpaceIndexPosition); // antes de hacer el movimiento, miramos la casilla desde la que arrancamos
            Space nextSpace = initialSpace; // referencia de la siguiente casilla a la que el jugador irá

            // recorremos casillas según espacios a moverse. En la primera vuelta del bucle, "nextSpace" será la casilla inicial. Después, serán las siguientes casillas de cada bucle respectivamente
            for (int i = 0; i < spaces; i++)
            {
                mPath.Add(nextSpace.SpaceOptions[0].gameObject.transform.GetChild(0).position);
                mPathSpaces.Add(nextSpace.SpaceOptions[0]);
                nextSpace = nextSpace.SpaceOptions[0];
            }

            TweenMovement(player, initialSpace);
        }

        public void Move(Player player, int spaces, int direction)
        {
            // inicializamos a vacío las rutas
            mPath = new List<Vector3>();
            mPathSpaces = new List<Space>();

            // casilla inicial y siguiente
            Space initialSpace = GSpaceCollection.Values.ElementAt(player.currentSpaceIndexPosition); // antes de hacer el movimiento, miramos la casilla desde la que arrancamos
            Space nextSpace = initialSpace; // referencia de la siguiente casilla a la que el jugador irá

            for (int i = 0; i < spaces; i++)
            {
                if (i == 0)
                {
                    mPath.Add(nextSpace.SpaceOptions[direction].gameObject.transform.GetChild(0).position);
                    mPathSpaces.Add(nextSpace.SpaceOptions[direction]);
                    nextSpace = nextSpace.SpaceOptions[direction];
                }
                else
                {
                    mPath.Add(nextSpace.SpaceOptions[0].gameObject.transform.GetChild(0).position);
                    mPathSpaces.Add(nextSpace.SpaceOptions[0]);
                    nextSpace = nextSpace.SpaceOptions[0];
                }
            }

            TweenMovement(player, initialSpace);
        }

        #endregion

        #region HANDLE MOVEMENT

        private void TweenMovement(Player player, Space initialSpace)
        {
            sequence = DOTween.Sequence();
            sequence.OnStart(() =>
            {
                HandleOnStart(player);

                if (mPath.Count > 0)
                {
                    Vector3 initialDirection = (mPath[0] - player.transform.position).normalized;
                    player.transform.DORotateQuaternion(Quaternion.LookRotation(initialDirection), 0.25f);
                }
            });

            indexWayPoint = 0; // Reiniciar el índice al inicio del movimiento

            for (int i = 0; i < mPath.Count; i++)
            {
                int currentIndex = i; // Capturar índice actual para OnComplete
                Vector3 currentPos = (i == 0) ? player.transform.position : mPath[i - 1];
                Vector3 targetPos = mPath[i];

                int jumps = CalculateJumps(currentPos, targetPos);

                sequence.Append(player.transform.DOJump(targetPos, 3, jumps, timeBySpace * jumps)
                    .SetEase(Ease.Linear)
                    .OnStart(() => AudioManager.Instance.PlayOneShot(FMODEvents.instance.eventData.jump))
                    .OnComplete(() =>
                    {
                        // Rotar hacia la siguiente dirección después de completar el salto
                        if (currentIndex + 1 < mPath.Count)
                        {
                            Vector3 nextDirection = (mPath[currentIndex + 1] - targetPos).normalized;
                            player.transform.DORotateQuaternion(Quaternion.LookRotation(nextDirection), 0.25f);
                        }

                        player.transform.DOShakeScale(.5f, .3f, 8);
                        HandleOnComplete(player, currentIndex);
                    }));
            }

            sequence.OnComplete(() => OnCompleteSequence(player));
            sequence.Play();
        }

        private int CalculateJumps(Vector3 start, Vector3 end)
        {
            float distance = Vector3.Distance(start, end);
            return Mathf.Max(1, Mathf.RoundToInt(distance / offsetBySpace));
        }

        private void HandleOnComplete(Player player, int waypointIndex)
        {
            Debug.Log("He caído en una nueva casilla");

            if (mPathSpaces[waypointIndex].data.typeSpace == SpaceData.TypeSpace.Choice)
            {
                player.currentSpaceIndexPosition = GSpaceCollection.Values.ToList().IndexOf(mPathSpaces[waypointIndex]);

                sequence.Kill();  // Detener la animación de movimiento
                OnPlayerRouteChoice?.Invoke(player); // Disparar evento de elección de ruta

                return;
            }

            player.nSpaceMoves--;

            AudioManager.Instance.PlayOneShot(FMODEvents.instance.eventData.land);

            indexWayPoint++;
        }

        private void OnCompleteSequence(Player player)
        {
            Space finalSpace;


            if (indexWayPoint < mPathSpaces.Count)
            {
                finalSpace = mPathSpaces[indexWayPoint];
            }
            else
            {
                finalSpace = mPathSpaces.Last();
            }

            player.currentSpaceIndexPosition = GSpaceCollection.Values.ToList().IndexOf(finalSpace);
            indexWayPoint = 0;

            if (finalSpace.data.typeSpace == SpaceData.TypeSpace.Choice)
            {
                Debug.Log("Jugador ha caído en una casilla Choice al final del movimiento");
                OnPlayerRouteChoice?.Invoke(player);
                return;
            }

            finalSpace.OnPlayerLands(player);

            OnFinishPlayerMove?.Invoke(player);

            OnPlayerLandsInSpace?.Invoke(player);

            if (!inEvent)
            {
                turn.ChangeTurn();
                OnStartTurn?.Invoke();
            }
        }

        private void HandleOnStart(Player player)
        {
            OnPlayerMovement?.Invoke(player);

            indexWayPoint = 0;
        }
        #endregion

        #region EVENTS

        private void StartEvent()
        {
            inEvent = true;
            OnEnterEvent?.Invoke();
        }

        private void FinishEvent()
        {
            inEvent = false;
            OnExitEvent?.Invoke();
        }

        #endregion
    }
}
