using System.Collections;
using System.Collections.Generic;
using ED.SC;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        private List<EventInstance> _instances;
        private Bus _masterBus;
        private Bus _musicBus;
        private Bus _sfxBus;
        private Bus _uiBus;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                
                _instances = new List<EventInstance>();
                _masterBus = RuntimeManager.GetBus("bus:/");
                _musicBus = RuntimeManager.GetBus("bus:/Music");
                _sfxBus = RuntimeManager.GetBus("bus:/SFX");
                _uiBus = RuntimeManager.GetBus("bus:/UI");
                
                return;
            }

            Destroy(gameObject);
        }

        private void Start()
        {
            PlayOnce(FModEvents.Instance.AmbienceMusic1, Vector2.zero);
        }

        public static void PlayOnce(EventReference @event, Vector2 worldPos)
        {
            RuntimeManager.PlayOneShot(@event, worldPos);
        }

        public static void PlayDelayed(EventReference @event, Vector2 worldPos, float delay)
        {
            Instance.StartCoroutine(PlayDelayedInternal(@event, worldPos, delay));
        }

        private static IEnumerator PlayDelayedInternal(EventReference @event, Vector2 worldPos, float delay)
        {
            yield return new WaitForSeconds(delay);
            RuntimeManager.PlayOneShot(@event, worldPos);
        }

        public static EventInstance CreateInstance(EventReference @event)
        {
            var instance = RuntimeManager.CreateInstance(@event);
            Instance._instances.Add(instance);
            return instance;
        }

        [Command]
        private void ChangeReverb(float value)
        {
            RuntimeManager.StudioSystem.setParameterByName("Reverb", Mathf.Clamp01(value));
        }

        [Command]
        private void SetMasterVolume(float value)
        {
            _masterBus.setVolume(Mathf.Clamp01(value));
        }

        [Command]
        private void SetSFXVolume(float value)
        {
            _sfxBus.setVolume(Mathf.Clamp01(value));
        }

        [Command]
        private void SetMusicVolume(float value)
        {
            _musicBus.setVolume(Mathf.Clamp01(value));
        }
        
        [Command]
        private void SetUIVolume(float value)
        {
            _uiBus.setVolume(Mathf.Clamp01(value));
        }
    }
}