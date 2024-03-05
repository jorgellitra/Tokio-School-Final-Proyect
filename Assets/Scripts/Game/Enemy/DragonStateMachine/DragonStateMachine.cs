using System.Collections.Generic;
using TokioSchool.FinalProject.Core;
using TokioSchool.FinalProject.Player;
using TokioSchool.FinalProject.UI;
using UnityEngine;
using UnityEngine.AI;

namespace TokioSchool.FinalProject.Enemy
{
    public class DragonStateMachine : StateManager<DragonStateMachine.DragonState>
    {
        public float distanceToChasePlayer = 10f;
        public float distanceToAttackPlayer = 1f;
        public float walkSpeed = 3f;
        public float runSpeed = 6f;
        public float waitingTimeIdle;

        public Transform[] patrolPoints;
        public float minDistanceToMoveNextPatrolPoint = 1;
        public bool playerInRangeToAttack;
        public bool playerInRangeToChase;

        [SerializeField] private UIController uiController;
        [SerializeField] private UIPanel resultPanel;

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

        public enum DragonState
        {
            Idle, Patrol, Chase, Attack, Dead
        }

        private void Awake()
        {
            anim = GetComponent<Animator>();
            navAgent = GetComponent<NavMeshAgent>();
            controller = GetComponent<EnemyController>();
            player = FindFirstObjectByType<PlayerController>();

            DragonIdleState enemyIdleState = new(DragonState.Idle, this);
            DragonPatrolState enemyPatrolState = new(DragonState.Patrol, this);
            DragonChaseState enemyChaseState = new(DragonState.Chase, this);
            DragonAttackState enemyAttackState = new(DragonState.Attack, this);
            DragonDeadState enemyDeadState = new(DragonState.Dead, this);

            states.Add(DragonState.Idle, enemyIdleState);
            states.Add(DragonState.Patrol, enemyPatrolState);
            states.Add(DragonState.Chase, enemyChaseState);
            states.Add(DragonState.Attack, enemyAttackState);
            states.Add(DragonState.Dead, enemyDeadState);

            currentState = states[DragonState.Idle];

            anim.SetFloat(animXDirection, 0);
            anim.SetFloat(animZDirection, 1);
        }

        private void FixedUpdate()
        {
            float distanceEnemyPlayer = Vector3.Distance(player.transform.position, transform.position);

            playerInRangeToChase = distanceEnemyPlayer < distanceToChasePlayer;
            playerInRangeToAttack = distanceEnemyPlayer < distanceToAttackPlayer;

            anim.SetFloat(animSpeed, navAgent.velocity.sqrMagnitude);
        }

        public void OnDeath()
        {
            uiController.SwitchScreens(resultPanel);
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0;
        }
    }
}
