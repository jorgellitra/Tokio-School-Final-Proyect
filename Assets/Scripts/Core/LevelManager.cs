using EasyTransition;
using System;
using UnityEngine;

namespace TokioSchool.FinalProject.Core
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private TransitionSettings transition;
        [SerializeField] private float startDelay;

        public void LoadScene(string scene)
        {
            TransitionManager.Instance().Transition(scene, transition, startDelay);
        }
    }
}
