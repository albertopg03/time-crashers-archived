using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Minigames/Data")]
public class MinigameData : ScriptableObject
{
    [Header("MINIGAMES")]
    [Space(5)]
    public List<Minigame> minigames;

    [Serializable]
    public class Minigame
    {
        public int id;
        public string name;
        public string sceneName;
    }
}
