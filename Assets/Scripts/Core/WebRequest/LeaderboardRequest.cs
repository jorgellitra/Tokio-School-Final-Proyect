using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TokioSchool.FinalProject.Core
{
    public class LeaderboardRequest : MonoBehaviour
    {
        [SerializeField] private string baseUrl;
        [SerializeField] private string getLeaderboardUrl;
        [SerializeField] private string postLeaderboardUrl;

        private Action<Response> onGetComplete;
        private Action<Response> onPostComplete;

        public Action<Response> OnGetComplete { get => onGetComplete; set => onGetComplete = value; }
        public Action<Response> OnPostComplete { get => onPostComplete; set => onPostComplete = value; }

        public void GetLeaderboad()
        {
            StartCoroutine(RestWebClient.Instance.HttpGet($"{baseUrl}/{getLeaderboardUrl}",
                (r) => onGetComplete?.Invoke(r)));
        }

        public void PostLeaderboard(Leaderboard leaderboard)
        {
            RequestHeader header = new RequestHeader
            {
                Key = "Content-Type",
                Value = "application/json"
            };

            StartCoroutine(RestWebClient.Instance.HttpPost($"{baseUrl}/{postLeaderboardUrl}",
                JsonConvert.SerializeObject(leaderboard), (r) => onPostComplete?.Invoke(r), new List<RequestHeader> { header }));
        }
    }
}