using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        private List<EventInstance> _instances;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                
                _instances = new List<EventInstance>();
                PlayOnce(FModEvents.Instance.AmbienceMusic1, Vector2.zero);
                
                return;
            }
            
            Destroy(gameObject);
        }

        public static void PlayOnce(EventReference @event, Vector2 worldPos)
        {
            RuntimeManager.PlayOneShot(@event, worldPos);
        }

        public static EventInstance CreateInstance(EventReference @event)
        {
            var instance = RuntimeManager.CreateInstance(@event);
            Instance._instances.Add(instance);
            return instance;
        }

        public static void ChangeReverb(float value)
        {
            RuntimeManager.StudioSystem.setParameterByName("Reverb", value);
        }
    }
}