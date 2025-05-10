using System;
using UnityEngine;

/// <summary>
/// Clase que contendrá todos los atributos necesarios para almacenar y gestionar las estadisticas
/// que consigue un jugador durante una partida.
/// </summary>
public class PlayerStats : MonoBehaviour
{
    // luego quitar del inspector
    [SerializeField] private int fragments; // numero de fragmentos que lleva el jugador

    public event Action<int> OnFragmentsChanged; // evento que se ejecuta cuando el numero de fragmentos ha cambiado

    public int Fragments
    {
        get => fragments;
        set
        {
            fragments = value;
            OnFragmentsChanged?.Invoke(fragments); 
        }
    }
}
