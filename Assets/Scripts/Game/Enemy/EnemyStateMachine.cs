using TokioSchool.FinalProject.Core;
using TokioSchool.FinalProject.Player;
using UnityEngine;

namespace TokioSchool.FinalProject.Enemy
{
    public class EnemyStateMachine : StateManager<EnemyStateMachine.EnemyState>
    {
        public float distanceToChasePlayer = 10f;

        private Animator anim;
        private Rigidbody rb;
        private PlayerController player;

        public Animator Anim { get => anim; }
        public Rigidbody Rb { get => rb; }
        public PlayerController Player { get => player; }

        public enum EnemyState
        {
            Idle, Patrol, Chase, Attack, Die
        }

        private void Awake()
        {
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            player = FindFirstObjectByType<PlayerController>();

            EnemyIdleState enemyIdleState = new(EnemyState.Idle, this);
            EnemyPatrolState enemyPatrolState = new(EnemyState.Patrol, this);

            states.Add(EnemyState.Idle, enemyIdleState);
            states.Add(EnemyState.Patrol, enemyPatrolState);

            currentState = states[EnemyState.Idle];
        }
    }
}
