using UnityEngine;
using System.Linq;

/// <summary>
/// Clase encargada de gestionar el estado que se ejecuta cuando el jugador esta eligiendo una ruta por la que ir
/// </summary>
public class PathChoiceState : IState
{
    private Player _player;

    private static readonly int Idle = Animator.StringToHash("Idle");

    private PathChoiceSpace currentSpace;

    /// <summary>
    /// Clase constructora al que se le pasa el contexto, que en este caso, sera el jugador
    /// </summary>
    /// <param name="player"></param>
    public PathChoiceState(Player player)
    {
        _player = player;
    }

    public void Enter()
    {
        // obtener una referencia de la casilla de eleccion en la que se cae
        currentSpace = _player.board.GSpaceCollection.Values.ToList()[_player.currentSpaceIndexPosition].GetComponent<PathChoiceSpace>();

        ActivatePathPosibilites(currentSpace, true);

        EventClick.OnSelectSpace += ChooseOption;
    }

    public void Update() { }

    /// <summary>
    /// Funcion encargada de permitir al jugador elegir una direccion por la que ir al caer en una casilla de eleccion de camino
    /// </summary>
    /// <param name="index"></param>
    private void ChooseOption(int index)
    {
        if (!currentSpace || currentSpace.ChoiceOptions.Count <= index) return;

        _player.board.Move(_player, _player.nSpaceMoves, index);
    }

    /// <summary>
    /// Funcìón encargada de gestionar la posibilidad de que el jugador elija una ruta respecto la casilla de elección en la que está
    /// </summary>
    private void ActivatePathPosibilites(PathChoiceSpace space, bool activate)
    {
        foreach(Space option in space.ChoiceOptions)
        {
            option.GetComponent<BoxCollider>().enabled = activate;
        }
    }

    public void Exit()
    {
        ActivatePathPosibilites(currentSpace, false);
        //Debug.Log("Jugador termin� de elegir una ruta.");
        EventClick.OnSelectSpace -= ChooseOption;
    }
}
