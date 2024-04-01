using System.Collections.Generic;
using TokioSchool.FinalProject.Core;
using TokioSchool.FinalProject.Player;
using UnityEngine;
using UnityEngine.AI;

namespace TokioSchool.FinalProject.Enemy
{
    public class EnemyStateMachine : StateManager<EnemyStateMachine.EnemyState>
    {
        public float distanceToChasePlayer = 10f;
        public float distanceToAttackPlayer = 1f;
        public float walkSpeed = 3f;
        public float runSpeed = 6f;
        public float waitingTimeIdle;

        public Transform[] patrolPoints;
        public BoxCollider[] weaponColliders;
        public float minDistanceToMoveNextPatrolPoint = 1;
        public bool playerInRangeToAttack;
        public bool playerInRangeToChase;

        private Animator anim;
        private NavMeshAgent navAgent;
        private EnemyController controller;
        private PlayerController player;

        public Animator Anim { get => anim; }
        public PlayerController Player { get => player; }
        public NavMeshAgent NavAgent { get => navAgent; }
        public EnemyController Controller { get => controller; }

        #region AnimHash

        [HideInInspector] public int animDeath = Animator.StringToHash("Death");
        [HideInInspector] public int animSpeed = Animator.StringToHash("Speed");
        [HideInInspector] public int animXDirection = Animator.StringToHash("XDirection");
        [HideInInspector] public int animZDirection = Animator.StringToHash("ZDirection");

        #endregion

        public enum EnemyState
        {
            Idle, Patrol, Chase, Attack, Dead
        }

        private void Awake()
        {
            anim = GetComponent<Animator>();
            navAgent = GetComponent<NavMeshAgent>();
            controller = GetComponent<EnemyController>();
            player = FindFirstObjectByType<PlayerController>();

            EnemyIdleState enemyIdleState = new(EnemyState.Idle, this);
            EnemyPatrolState enemyPatrolState = new(EnemyState.Patrol, this);
            EnemyChaseState enemyChaseState = new(EnemyState.Chase, this);
            EnemyAttackState enemyAttackState = new(EnemyState.Attack, this);
            EnemyDeadState enemyDeadState = new(EnemyState.Dead, this);

            states.Add(EnemyState.Idle, enemyIdleState);
            states.Add(EnemyState.Patrol, enemyPatrolState);
            states.Add(EnemyState.Chase, enemyChaseState);
            states.Add(EnemyState.Attack, enemyAttackState);
            states.Add(EnemyState.Dead, enemyDeadState);

            currentState = states[EnemyState.Idle];

            anim.SetFloat(animXDirection, 0);
            anim.SetFloat(animZDirection, 1);
        }

        private void FixedUpdate()
        {
            float distanceEnemyPlayer = Vector3.Distance(player.transform.position, transform.position);

            playerInRangeToChase = distanceEnemyPlayer < distanceToChasePlayer && !player.Dead;
            playerInRangeToAttack = distanceEnemyPlayer < distanceToAttackPlayer && !player.Dead;

            anim.SetFloat(animSpeed, navAgent.velocity.sqrMagnitude);
        }
    }
}
