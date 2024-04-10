using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TokioSchool.FinalProject.Core;
using UnityEngine;
using UnityEngine.UI;

namespace TokioSchool.FinalProject.UI
{
    public class UIResultController : UIPanel
    {
        [SerializeField] private LeaderboardRequest leaderboardRequest;
        [SerializeField] private TextMeshProUGUI timeResultText;
        [SerializeField] private TextMeshProUGUI positionText;
        [SerializeField] private TMP_InputField playerNameInput;

        private Leaderboard leaderboardToSave = new Leaderboard();

        private void OnEnable()
        {
            if (leaderboardRequest != null)
            {
                leaderboardRequest.OnGetComplete += OnGetComplete;
                leaderboardRequest.OnPostComplete += OnPostComplete;
            }
        }

        private void OnDisable()
        {
            if (leaderboardRequest != null)
            {
                leaderboardRequest.OnGetComplete -= OnGetComplete;
                leaderboardRequest.OnPostComplete -= OnPostComplete;
            }
        }

        public override void StartScreen()
        {
            base.StartScreen();

            leaderboardRequest.GetLeaderboad();
        }

        private void OnGetComplete(Response response)
        {
            List<Leaderboard> leaderboards = JsonConvert.DeserializeObject<List<Leaderboard>>(response.Data);
            PlayerData data = PlayerPrefsManager.Instance.Load();

            //plus one because it start from 0
            var position = leaderboards.FindIndex(l => l.Miliseconds >= data.miliseconds) + 1;

            TimeSpan time = TimeSpan.FromMilliseconds(data.miliseconds);
            timeResultText.text = $"{time.Minutes:00}:{time.Seconds:00}:{time.Milliseconds:000}";

            positionText.text = position.ToString();
            leaderboardToSave.Miliseconds = (long)data.miliseconds;
        }

        public void SaveRecord()
        {
            leaderboardToSave.Name = playerNameInput.text;
            leaderboardRequest.PostLeaderboard(leaderboardToSave);
        }

        private void OnPostComplete(Response response)
        {
            if (bool.Parse(response.Data))
            {
                LevelManager.Instance.LoadNextLevel(0);
            }
        }
    }
}
