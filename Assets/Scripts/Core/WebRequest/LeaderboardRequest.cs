using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TokioSchool.FinalProject.Configs;
using UnityEngine;
using UnityEngine.Events;

namespace TokioSchool.FinalProject.Core
{
    public class LeaderboardRequest : MonoBehaviour
    {
        [SerializeField] private AzureConfig azureConfig;

        private Action<Response> onGetComplete;
        private Action<Response> onPostComplete;

        public Action<Response> OnGetComplete { get => onGetComplete; set => onGetComplete = value; }
        public Action<Response> OnPostComplete { get => onPostComplete; set => onPostComplete = value; }

        public void GetLeaderboad()
        {
            StartCoroutine(RestWebClient.Instance.HttpGet($"{azureConfig.BaseUrl}/{azureConfig.GetLeaderboard}",
                (r) => onGetComplete?.Invoke(r)));
        }

        public void PostLeaderboard(Leaderboard leaderboard)
        {
            RequestHeader header = new RequestHeader
            {
                Key = "Content-Type",
                Value = "application/json"
            };

            StartCoroutine(RestWebClient.Instance.HttpPost($"{azureConfig.BaseUrl}/{azureConfig.PostLeaderboard}",
                JsonConvert.SerializeObject(leaderboard), (r) => onPostComplete?.Invoke(r), new List<RequestHeader> { header }));
        }
    }
}