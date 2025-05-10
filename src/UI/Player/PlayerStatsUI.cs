using UnityEngine;
using TMPro;

/// <summary>
/// Clase controaldora de la UI relacionada con el jugador
/// </summary>
public class PlayerStatsUI : MonoBehaviour
{
    [Header("UI ELEMENTS")]
    [Space(10)]
    [SerializeField] private TMP_Text fragmentsText;

    private PlayerStats playerStats;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    private void OnEnable()
    {
        playerStats.OnFragmentsChanged += UpdateUI;
    }

    private void OnDisable()
    {
        playerStats.OnFragmentsChanged -= UpdateUI;
    }

    private void Start()
    {
        UpdateUI(playerStats.Fragments);
    }

    /// <summary>
    /// Función encargada de actualizar el contador de fragmentos de los jugadores
    /// en el momento en el que se reciba una señal de que el numero de fragmentos ha cambiado
    /// </summary>
    /// <param name="newFragmentCount"></param>
    private void UpdateUI(int newFragmentCount)
    {
        Debug.Log("Actualiza UI");
        fragmentsText.text = newFragmentCount.ToString();
    }
}
