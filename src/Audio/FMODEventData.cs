using FMODUnity;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/FMOD Event Data")]
public class FMODEventData : ScriptableObject
{
    public EventReference music;
    public EventReference ambience;
    public EventReference jump;
    public EventReference land;
}
