using System.Collections;
using System.Collections.Generic;
using TokioSchool.FinalProject.Singletons;
using UnityEngine;
using UnityEngine.Audio;

namespace TokioSchool.FinalProject.Core
{
    public class AudioManager : Singleton<AudioManager>
    {
        private const string AMBIENT_VOLUME = "AmbientVolume";
        private const string EFFECT_VOLUME = "EffectVolume";
        private const string UI_VOLUME = "UIVolume";

        [SerializeField] private AudioMixer audioMixer;

        private void Start()
        {
            if (SettingsAreSaved())
            {
                LoadSettings();
            }
        }

        public void SetAmbientVolume(float volumen)
        {
            audioMixer.SetFloat(AMBIENT_VOLUME, Mathf.Log10(volumen) * 20);
            PlayerPrefs.SetFloat(AMBIENT_VOLUME, volumen);
            PlayerPrefs.SetInt("optionsAudio", 1);
        }

        public void SetEffectVolume(float volumen)
        {
            audioMixer.SetFloat(EFFECT_VOLUME, Mathf.Log10(volumen) * 20);
            PlayerPrefs.SetFloat(EFFECT_VOLUME, volumen);
            PlayerPrefs.SetInt("optionsAudio", 1);
        }

        public void SetUIVolume(float volumen)
        {
            audioMixer.SetFloat(UI_VOLUME, Mathf.Log10(volumen) * 20);
            PlayerPrefs.SetFloat(UI_VOLUME, volumen);
            PlayerPrefs.SetInt("optionsAudio", 1);
        }

        public float GetAmbientVolume()
        {
            return PlayerPrefs.GetFloat(AMBIENT_VOLUME, 1);
        }

        public float GetEffectVolume()
        {
            return PlayerPrefs.GetFloat(EFFECT_VOLUME, 1);
        }

        public float GetUIVolume()
        {
            return PlayerPrefs.GetFloat(UI_VOLUME, 1);
        }


        public void LoadSettings()
        {
            float ambientVolume = Mathf.Log10(GetAmbientVolume()) * 20;
            float effectVolume = Mathf.Log10(GetEffectVolume()) * 20;
            float uiVolume = Mathf.Log10(GetUIVolume()) * 20;

            audioMixer.SetFloat(AMBIENT_VOLUME, ambientVolume);
            audioMixer.SetFloat(EFFECT_VOLUME, effectVolume);
            audioMixer.SetFloat(UI_VOLUME, uiVolume);
        }

        private bool SettingsAreSaved()
        {
            return PlayerPrefs.GetInt("optionsAudio") == 1;
        }
    }
}
