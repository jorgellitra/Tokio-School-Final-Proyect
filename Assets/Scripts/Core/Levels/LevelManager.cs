using EasyTransition;
using System;
using TokioSchool.FinalProject.Enums;
using TokioSchool.FinalProject.Singletons;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TokioSchool.FinalProject.Core
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private TransitionSettings transition;

        public bool StopCountingTime = false;

        public float miliseconds = 0;
        private PlayerData data;

        private void Start()
        {
            StopCountingTime = false;
            miliseconds = 0;
        }

        public void LoadScene(string scene)
        {
            TransitionManager.Instance().Transition(scene, transition, 0);
        }

        public void LoadNextLevel(int delay)
        {
            ELevels currentLevel = Enum.Parse<ELevels>(SceneManager.GetActiveScene().name);
            ELevels nextScene = ELevels.MainMenu;

            data = PlayerPrefsManager.Instance.Load();

            switch (currentLevel)
            {
                case ELevels.MainMenu:
                    data.miliseconds = 0;
                    data.weapon3IsLocked = true;
                    data.weapon4IsLocked = true;
                    nextScene = ELevels.Minotaur;
                    break;
                case ELevels.Minotaur:
                    data.miliseconds = 0;
                    data.weapon3IsLocked = true;
                    data.weapon4IsLocked = true;
                    data.weapon3IsLocked = false;
                    nextScene = ELevels.Zombies;
                    break;
                case ELevels.Zombies:
                    data.miliseconds += miliseconds;
                    data.weapon4IsLocked = false;
                    nextScene = ELevels.Dragon;
                    break;
                case ELevels.Dragon:
                    Time.timeScale = 1;
                    nextScene = ELevels.MainMenu;
                    break;
            }
            PlayerPrefsManager.Instance.Save(data);
            string levelName = Enum.GetName(typeof(ELevels), nextScene);
            StopCountingTime = true;
            miliseconds = 0;
            TransitionManager.Instance().Transition(levelName, transition, delay);
        }

        public ELevels CurrentLevel()
        {
            return Enum.Parse<ELevels>(SceneManager.GetActiveScene().name);
        }

        public void SaveTime()
        {
            StopCountingTime = true;
            data = PlayerPrefsManager.Instance.Load();
            data.miliseconds += miliseconds;
            PlayerPrefsManager.Instance.Save(data);
        }

        public void ResetTime()
        {
            PlayerPrefsManager.Instance.ResetData();
        }

        private void Update()
        {
            if (!StopCountingTime)
            {
                miliseconds += Time.deltaTime * 1000;
            }
        }
    }
}
