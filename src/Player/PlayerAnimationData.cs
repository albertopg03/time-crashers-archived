using System;
using UnityEngine;

/// <summary>
/// Scriptable Object (SO) que contiene la configuracion de las animaciones de los jugadores
/// </summary>
[CreateAssetMenu(menuName = "Player/Animation", order = 1)]
public class PlayerAnimationData : ScriptableObject
{
    [Header("WalkingAnim")]
    [Space(10)]
    [Tooltip("Duracion de la animacion de andar")]
    public float _fixedTransitionDurationWalk;
    
    [Header("IdleAnim")]
    [Space(10)]
    [Tooltip("Duracion de la animacion de estar quieto")]
    public float _fixedTransitionDurationIdle;

}
