using System;
using System.Dynamic;
using UnityEngine;
using FMOD;
using FMOD.Studio;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
   public static AudioManager Instance;
   
   private EventInstance musicEventInstance;

   private EventInstance ambienceEventInstance;
   
   public EventInstance MusicEventInstance => musicEventInstance;

  private void Awake()
  {
     if (Instance == null)
     {
         Instance = this;
     }
     else
     {
        Destroy(gameObject);
     }
  }

  private void Start()
   {
      InitializeMusic(FMODEvents.instance.eventData.music);
      InitializeAmbience(FMODEvents.instance.eventData.ambience);
   }
   
   #region One Shots Sounds
   
   public void PlayOneShot(EventReference sound, Vector3 worldPosition)
   {
      RuntimeManager.PlayOneShot(sound, worldPosition);
   }
   
   public void PlayOneShot(EventReference sound)
   {
      RuntimeManager.PlayOneShot(sound);
   }
   
   #endregion
   
   #region Event Instances
   public void InitializeMusic(EventReference musicEventReference)
   {
      musicEventInstance = CreateInstance(musicEventReference);
      musicEventInstance.start();
   }
   public void InitializeAmbience(EventReference ambienceEventReference)
   {
      ambienceEventInstance = CreateInstance(ambienceEventReference);
      ambienceEventInstance.start();
   }

   public EventInstance CreateInstance(EventReference eventReference)
   {
      EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
      return eventInstance;
   }
   #endregion
   
   #region LabelParameters
   public void TriggerLabelChange(EventInstance eventInstanceReference,string parameterName, int labelValue)
   {
      FMODParameterData parameterData = FMODEvents.instance.parameterData;

      foreach (var label in parameterData.parameterLabelChanges)
      {
         if (label.parameterName == parameterName)
         {
            SetParameterByLabel(eventInstanceReference, label.parameterName, label.inEventLabel[labelValue]);
         }
      }
   }

   public void ResetLabel(EventInstance eventInstanceReference,string parameterName)
   {
      FMODParameterData parameterData = FMODEvents.instance.parameterData;

      foreach (var label in parameterData.parameterLabelChanges)
      {
         if (label.parameterName == parameterName)
         {
            SetParameterByLabel(eventInstanceReference,label.parameterName, label.normalLabel);
         }
      }
   }
   public void SetParameterByLabel(EventInstance eventInstanceReference,string parameterName, string label)
   {
      if (eventInstanceReference.isValid())
      {
         eventInstanceReference.setParameterByNameWithLabel(parameterName, label);
      }
   }
   #endregion
   
   #region ValueParameters
   public void TriggerParameterChange(EventInstance eventInstanceReference, string parameterName)
   {
      FMODParameterData parameterData = FMODEvents.instance.parameterData;

      foreach (var param in parameterData.parameterValueChanges)
      {
         if (param.parameterName == parameterName)
         {
            SetParameterByName(eventInstanceReference,param.parameterName, param.inEventValue);
         }
      }
   }
   public void ResetParameter(EventInstance eventInstanceReference, string parameterName)
   {
      FMODParameterData parameterData = FMODEvents.instance.parameterData;

      foreach (var param in parameterData.parameterValueChanges)
      {
         if (param.parameterName == parameterName)
         {
            SetParameterByName(eventInstanceReference,param.parameterName, param.normalValue);
         }
      }
   }
   public void SetParameterByName(EventInstance eventInstanceReference, string parameterName, float value)
   {
      if (eventInstanceReference.isValid())
      {
         eventInstanceReference.setParameterByName(parameterName, value);
      }
   }
   #endregion
   
   #region Destroy Event Instances
   private void OnDestroy()
   {
      if (musicEventInstance.isValid())
      {
         musicEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
         musicEventInstance.release();
      }

      if (ambienceEventInstance.isValid())
      {
         ambienceEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
         ambienceEventInstance.release();
      }
   }
   #endregion
   
}
