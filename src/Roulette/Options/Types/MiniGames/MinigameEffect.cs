using System.Collections;
using UnityEngine;

public class MinigameEffect : EffectOptionRoulete, IMinigame
{
    [Header("DATA")]
    [Space(5)]
    public OptionModel model;

    private MinigameData.Minigame selectedMinigame;

    public MinigameData.Minigame Minigame => selectedMinigame;

    public string SceneMinigameName => selectedMinigame.sceneName;

    private void Awake()
    {
        model = GetComponent<OptionModel>();
    }

    private void OnEnable()
    {
        PopulationOptions.OnOptionsUpdated += InitializeValue;
        StartCoroutine(DelayedInitializeValue());
    }

    private void OnDisable()
    {
        PopulationOptions.OnOptionsUpdated -= InitializeValue;
    }

    private IEnumerator DelayedInitializeValue()
    {
        yield return new WaitForSeconds(0.5f);
        InitializeValue();
    }

    private void InitializeValue()
    {
        selectedMinigame = GetRandomMinigame();
    }

    private MinigameData.Minigame GetRandomMinigame()
    {
        return model.data.minigamesList.minigames[Random.Range(0, model.data.minigamesList.minigames.Count)];
    }

    public override void ApplyEffect(Player player)
    {
        // descomentar cuando se pruebe la BUILD en móvil
        //UnityEngine.SceneManagement.SceneManager.LoadScene(SceneMinigameName);
    }
}
