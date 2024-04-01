using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TokioSchool.FinalProject.Core
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private UnityEvent onStep;

        public void OnStep()
        {
            onStep?.Invoke();
        }
    }
}