using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TokioSchool.FinalProject.Configs
{
    [CreateAssetMenu(menuName = ("Config/AzureConfig"))]
    public class AzureConfig : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] string baseUrl;
        [SerializeField] string getLeaderboard;
        [SerializeField] string postLeaderboard;

        public string BaseUrl { get => baseUrl; }
        public string GetLeaderboard { get => getLeaderboard; }
        public string PostLeaderboard { get => postLeaderboard; }

        public void OnAfterDeserialize()
        {
        }

        public void OnBeforeSerialize()
        {
        }
    }
}
