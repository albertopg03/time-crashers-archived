using UnityEngine;
using TMPro;

[CreateAssetMenu(menuName = "Roulette/Option/Option Data", order = 1)]
public class RouletteOptionData : ScriptableObject
{
    [Header("IMAGE ICON")]
    [Space(5)]
    public TMP_SpriteAsset spriteAsset;
    public int indexImage;
    public IconSize iconSize;
    public enum IconSize
    {
        Base = 100,
        Sm = 50,
        Md = 200,
        Lg = 300,
        Lg2 = 400,
        Lg3 = 500
    }

    [Header("TEXTS")]
    [Space(5)]
    public string prependText;
    public string apendText;

    [Header("VALUES")]
    [Space(5)]
    public int minValue;
    public int maxValue;

    [Header("ITEM")]
    [Space(5)]
    public ItemData item;

    [Header("MINIGAMES")]
    [Space(5)]
    public MinigameData minigamesList; 


    public string GetTextData(int value)
    {
        return $"{prependText} {value} <size={(int)iconSize}%><sprite=\"{spriteAsset.name}\" index={indexImage}></size> {apendText}";
    }
}