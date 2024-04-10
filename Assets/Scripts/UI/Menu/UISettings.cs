using TMPro;
using TokioSchool.FinalProject.Core;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace TokioSchool.FinalProject.UI
{
    public class UISettings : UIPanel
    {
        [SerializeField] private Slider ambientSound;
        [SerializeField] private Slider effectSound;
        [SerializeField] private Slider interfaceSound;
        [SerializeField] private TMP_Dropdown dropdownResolutions;
        [SerializeField] private TMP_Dropdown dropdownQuality;
        [SerializeField] private Toggle toggleFullscreen;


        private void OnEnable()
        {
            dropdownQuality.onValueChanged.AddListener(SetQuality);
            dropdownResolutions.onValueChanged.AddListener(SetResolution);
            toggleFullscreen.onValueChanged.AddListener(IsFullScreen);
            ambientSound.onValueChanged.AddListener(AudioManager.Instance.SetAmbientVolume);
            effectSound.onValueChanged.AddListener(AudioManager.Instance.SetEffectVolume);
            interfaceSound.onValueChanged.AddListener(AudioManager.Instance.SetUIVolume);
        }

        private void OnDisable()
        {
            dropdownQuality.onValueChanged.RemoveAllListeners();
            dropdownResolutions.onValueChanged.RemoveAllListeners();
            toggleFullscreen.onValueChanged.RemoveAllListeners();
            ambientSound.onValueChanged.RemoveAllListeners();
            effectSound.onValueChanged.RemoveAllListeners();
            interfaceSound.onValueChanged.RemoveAllListeners();
        }

        public override void StartScreen()
        {
            base.StartScreen();

            InitializeResolutionDropdow();
            if (OptionsAreSaved())
            {
                LoadOptions();
            }
        }

        private void InitializeResolutionDropdow()
        {
            dropdownResolutions.ClearOptions();
            Resolution[] resolutions = Screen.resolutions;

            for (int i = Screen.resolutions.Length - 1; i >= 0; i--)
            {
                TMP_Dropdown.OptionData optionData = new()
                {
                    text = resolutions[i].width + "x" + resolutions[i].height
                };
                dropdownResolutions.options.Add(optionData);
                if (PlayerPrefs.GetInt("resolution", -1) == -1 && Screen.currentResolution.width == resolutions[i].width && Screen.currentResolution.height == resolutions[i].height)
                {
                    dropdownResolutions.value = i;
                }
            }
            dropdownResolutions.RefreshShownValue();
        }

        public void SetQuality(int quality)
        {
            PlayerPrefs.SetInt("quality", quality);
            PlayerPrefs.SetInt("options", 1);
            QualitySettings.SetQualityLevel(quality);
        }

        public void IsFullScreen(bool active)
        {
            PlayerPrefs.SetInt("fullscreen", active ? 1 : 0);
            PlayerPrefs.SetInt("options", 1);
            Screen.fullScreen = active;
        }

        public void SetResolution(int value)
        {
            PlayerPrefs.SetInt("resolution", value);
            PlayerPrefs.SetInt("options", 1);
            int width = Screen.resolutions[value].width;
            int height = Screen.resolutions[value].height;
            Screen.SetResolution(width, height, Screen.fullScreen);
        }

        public void LoadOptions()
        {
            dropdownResolutions.value = PlayerPrefs.GetInt("resolution");
            toggleFullscreen.isOn = PlayerPrefs.GetInt("fullscreen") == 1;
            dropdownQuality.value = PlayerPrefs.GetInt("quality");
            ambientSound.value = AudioManager.Instance.GetAmbientVolume();
            effectSound.value = AudioManager.Instance.GetEffectVolume();
            interfaceSound.value = AudioManager.Instance.GetUIVolume();
        }

        private bool OptionsAreSaved()
        {
            return PlayerPrefs.GetInt("options") == 1;
        }
    }
}
