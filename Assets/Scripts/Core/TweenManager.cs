using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TokioSchool.FinalProject.Singletons;
using UnityEngine;


namespace TokioSchool.FinalProject.Tweens
{
    public class TweenManager : Singleton<TweenManager>
    {
        private void Awake()
        {
            DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
        }

        public void DoSequence(List<Tween> tweens, Action action = null)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.OnComplete(() =>
            {
                action?.Invoke();
            });

            foreach (Tween t in tweens)
            {
                sequence.Append(t);
            }

            sequence.Play();
        }
    }
}
