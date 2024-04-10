using System.Collections;
using System.Collections.Generic;
using TokioSchool.FinalProject.Enemy;
using UnityEngine;
using UnityEngine.Events;

namespace TokioSchool.FinalProject.Enemy
{
    public class DragonAnimEventController : EnemyAnimEventController
    {
        [SerializeField] private AudioClip onAttack3Start;
        [SerializeField] private AudioClip onAttack3End;
        [SerializeField] private AudioClip onScream;
        [SerializeField] private AudioClip onFly;

        private DragonStateMachine stateMachine;

        private void Awake()
        {
            stateMachine = GetComponent<DragonStateMachine>();
        }

        public void OnAttack3Start()
        {
            stateMachine.flameAtacckEffect.Play();
            PlayEffect(onAttack3Start);
        }

        public void OnAttack3End()
        {
            stateMachine.flameAtacckEffect.Stop();
            PlayEffect(onAttack3End);
        }

        public void OnScream()
        {
            PlayEffect(onScream);
        }

        public void OnFly()
        {
            PlayEffect(onFly);
        }
    }
}