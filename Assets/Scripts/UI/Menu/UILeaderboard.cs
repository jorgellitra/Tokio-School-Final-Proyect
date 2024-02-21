using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TokioSchool.FinalProject.Core;
using UnityEngine;

namespace TokioSchool.FinalProject.UI
{
    public class UILeaderboard : UIPanel
    {
        [SerializeField] private GameObject playerRankingPrefab;
        [SerializeField] private Transform leaderboardContainer;
        [SerializeField] private LeaderboardRequest leaderboardRequest;

        private void OnEnable()
        {
            leaderboardRequest.OnGetComplete += OnGetComplete;
        }

        private void OnDisable()
        {
            leaderboardRequest.OnGetComplete -= OnGetComplete;
        }
        public override void StartScreen()
        {
            base.StartScreen();
            leaderboardContainer.DestroyChildren();
            //leaderboardRequest.PostLeaderboard(new Leaderboard() { Name = "new boi", Miliseconds = 49009 });
        }

        private void OnGetComplete(Response response)
        {
            List<Leaderboard> leaderboards = JsonConvert.DeserializeObject<List<Leaderboard>>(response.Data);
            foreach (Leaderboard leaderboard in leaderboards)
            {
                PlayerRaking ranking = Instantiate(playerRankingPrefab, leaderboardContainer).GetComponent<PlayerRaking>();
                ranking.Setup(leaderboard.Name, leaderboard.Miliseconds);
            }
        }
    }
}