using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TokioSchool.FinalProject.Singletons;
using UnityEngine;

namespace TokioSchool.FinalProject.Core
{
    [Serializable]
    public class ProjectilesWeaponData
    {
        public int CurrentNumberOfProjectiles;
        public int CurrentProjectilesLoaded;
    }

    public class PlayerData
    {
        public bool weapon1IsLocked = false;
        public bool weapon2IsLocked = false;
        public bool weapon3IsLocked = true;
        public bool weapon4IsLocked = true;
        public float miliseconds = 0;
        public Dictionary<string, ProjectilesWeaponData> weaponsData = new();
    }

    public class PlayerPrefsManager : Singleton<PlayerPrefsManager>
    {
        private const string PLAYER_DATA = "PlayerData";

        public PlayerData Load()
        {
            string data = PlayerPrefs.GetString(PLAYER_DATA);

            if (string.IsNullOrEmpty(data))
            {
                data = JsonConvert.SerializeObject(new PlayerData());
            }

            return JsonConvert.DeserializeObject<PlayerData>(data);
        }

        public void Save(PlayerData playerData = null)
        {
            PlayerPrefs.SetString(PLAYER_DATA, JsonConvert.SerializeObject(playerData));
        }

        public void Save()
        {
            PlayerPrefs.SetString(PLAYER_DATA, JsonConvert.SerializeObject(Load()));
        }

        public void ResetData()
        {
            PlayerData data = new();
            Save(data);
        }
    }
}
