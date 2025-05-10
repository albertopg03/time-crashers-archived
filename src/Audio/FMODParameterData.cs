using UnityEngine;

[CreateAssetMenu(menuName = "Audio/FMOD Dynamic Parameter Data")]
public class FMODParameterData : ScriptableObject
{
    [System.Serializable]
    public class ParameterChange
    {
        public string parameterName;  
        public float normalValue;
        public float inEventValue;
    }

    [System.Serializable]
    public class ParameterLabelChange
    {
        public string parameterName;  
        public string normalLabel;    
        public string[] inEventLabel;   // Label cuando ocurre algun event
    }
    
    public ParameterChange[] parameterValueChanges;
    public ParameterLabelChange[] parameterLabelChanges;

}

