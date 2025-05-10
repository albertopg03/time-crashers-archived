using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Board;
using UnityEngine.UI;
using System;

public class RouletteUI : MonoBehaviour
{
    private List<RectTransform> optionUIElements = new List<RectTransform>();
    private RouletteController rouletteController;

    [Header("BOARD")]
    [Space(10)]
    [SerializeField] private BoardController board;

    [Header("UI")]
    [Space(10)]
    [SerializeField] private RectTransform cursor;

    // animation props
    [Header("ANIMATION PROPS")]
    [Space(10)]
    [SerializeField] private float timeMaxAnimation;
    [SerializeField] private float initialTimeBetweenOption;
    private float timeBetweenOption;
    [SerializeField] private float offset = 0.35f;
    [Range(1.2f, 1.5f)][SerializeField] private float speedReductionIndex;

    // actions
    public event Action OnStartRoulete;
    public event Action OnFinishRoulete;

    // Opcion seleccionada
    private RectTransform optionSelected;

    [SerializeField] private RouletteManager rouletteManager;

    private void Awake()
    {
        rouletteController = GetComponent<RouletteController>();
    }

    private void OnEnable()
    {
        rouletteManager.OnRouletteManagerReady += InitializeUIElements;
    }

    private void OnDisable()
    {
        if (!optionSelected) return;

        optionSelected.localScale = Vector3.one;

        rouletteManager.OnRouletteManagerReady -= InitializeUIElements;
    }

    private void InitializeUIElements()
    {
        cursor.gameObject.SetActive(true);

        optionUIElements.Clear();

        foreach (EffectOptionRoulete option in rouletteController.effectOptions)
        {
            RectTransform uiElement = option.GetComponent<RectTransform>();

            if (uiElement != null)
            {
                optionUIElements.Add(uiElement);
            }
        }

        StartCoroutine(AnimationCoroutine());
    }

    private IEnumerator AnimationCoroutine()
    {
        OnStartRoulete?.Invoke();

        timeBetweenOption = initialTimeBetweenOption;

        int nLoops = (int) Math.Round(timeMaxAnimation / (initialTimeBetweenOption * optionUIElements.Count), MidpointRounding.AwayFromZero);

        for (int i = 0; i < nLoops; i++)
        {
            for (int j = 0; j < optionUIElements.Count; j++)
            {
                OptionEffect(optionUIElements[j]);

                yield return new WaitForSeconds(timeBetweenOption);
            }

            timeBetweenOption *= speedReductionIndex;
        }

        int indexOptionSelected = rouletteController.GetIndexOptionSelected();
        for (int i = 0; i <= indexOptionSelected; i++)
        {
            OptionEffect(optionUIElements[i]);

            yield return new WaitForSeconds(timeBetweenOption);
        }

        optionSelected = optionUIElements[indexOptionSelected];
        OptionsSelectedEffect(optionSelected);

        yield return new WaitForSeconds(.5f);

        DesactivateUI();
    }

    private void OptionEffect(RectTransform option)
    {
        cursor.anchoredPosition = option.anchoredPosition - new Vector2(option.rect.width * offset, 0);

        option.DOScale(1.1f, 0.3f).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo);
    }

    private void OptionsSelectedEffect(RectTransform option)
    {
        cursor.gameObject.SetActive(false);

        option.DOScale(1.2f, 0.5f).SetEase(Ease.OutBounce);
    }

    private void DesactivateUI()
    {
        OnFinishRoulete?.Invoke();

        gameObject.SetActive(false);
    }
}
