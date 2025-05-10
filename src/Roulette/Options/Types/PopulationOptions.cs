using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationOptions : MonoBehaviour
{
    public static event System.Action OnOptionsUpdated;

    [SerializeField] private RouletteManager rouletteManager;

    private void OnEnable()
    {
        StartCoroutine(PopulateRandomOptions());
    }

    private IEnumerator PopulateRandomOptions()
    {
        Debug.Log("CANTIDAD ELEMENTOS: " + rouletteManager.rouletteOptionsCore[rouletteManager.currentIndexPosition].optionList.Count);

        for (int i = 0; i < rouletteManager.rouletteOptionsCore[rouletteManager.currentIndexPosition].optionList.Count; i++)
        {
            rouletteManager.rouletteOptionsCore[rouletteManager.currentIndexPosition].optionList[i].data = rouletteManager.rouletteOptionsCore[rouletteManager.currentIndexPosition].dataList[Random.Range(0, rouletteManager.rouletteOptionsCore[rouletteManager.currentIndexPosition].dataList.Count)];
        }

        yield return new WaitForSeconds(0.5f);

        OnOptionsUpdated?.Invoke();
    }
}
