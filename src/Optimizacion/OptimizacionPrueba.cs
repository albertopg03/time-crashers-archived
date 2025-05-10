using System;
using DG.Tweening;
using UnityEngine;

public class OptimizacionPrueba : MonoBehaviour
{

    public GameObject cube;
    
    public Transform target;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 2;
    }

    private void Update()
    {
        Debug.Log(Application.targetFrameRate);
    }


    public void MoveGO()
    {
        cube.transform.DOMove(target.position, 0.5f);
    }
}