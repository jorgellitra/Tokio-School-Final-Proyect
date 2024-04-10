using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TokioSchool.FinalProject.Core;
using TokioSchool.FinalProject.Player;
using UnityEngine;
using UnityEngine.AI;

namespace TokioSchool.FinalProject.Enemy
{
    public class DragonStateMachine : StateManager<DragonStateMachine.DragonStates>
    {
        public float distanceToChasePlayer = 10f;
        public float distanceToBasicAttack = 3f;
        public float distanceToClawAttack = 6f;
        public float distanceToFlyFlameAttack = 15f;
        public float distanceToFlameAttack = 10f;
        public float walkSpeed = 3f;
        public float runSpeed = 6f;
        public float waitingTimeIdle;
        public ParticleSystem flameAtacckEffect;

        public bool playerInRangeToAttack;
        public bool playerInRangeToChase;
        public bool isTransitioning;
        public DragonAttacks nextAttack;
        public Dictionary<DragonAttacks, float> coldownAttacks;
        public float coldownTimeAttack = 2f;
        public BoxCollider[] weaponColliders;

        private Animator anim;
        private NavMeshAgent navAgent;
        private EnemyController controller;
        private PlayerController player;
        private Dictionary<int, DragonAttacks> attacksAvailable;
        private float nextAttackDistance;
        private int nextAttackStartIndex = 0;
        private DragonPhases currentPhase = DragonPhases.First;

        public Animator Anim { get => anim; }
        public PlayerController Player { get => player; }
        public NavMeshAgent NavAgent { get => navAgent; }
        public EnemyController Controller { get => controller; }
        public DragonAttacks NextAttack { get => nextAttack; }
        public DragonPhases CurrentPhase { get => currentPhase; }

        #region AnimHash

        [HideInInspector] public int animSpeed = Animator.StringToHash("Speed");
        [HideInInspector] public int animFlying = Animator.StringToHash("Flying");
        [HideInInspector] public int animTransitioning = Animator.StringToHash("Transitioning");
        [HideInInspector] public int animScream = Animator.StringToHash("Scream");
        [HideInInspector] public int animHit = Animator.StringToHash("Hit");
        [HideInInspector] public int animDead = Animator.StringToHash("Dead");
        [HideInInspector] public int animLand = Animator.StringToHash("Land");
        [HideInInspector] public int animAttack1 = Animator.StringToHash("Attack1");
        [HideInInspector] public int animAttack2 = Animator.StringToHash("Attack2");
        [HideInInspector] public int animAttack3 = Animator.StringToHash("Attack3");
        [HideInInspector] public int animAttack4 = Animator.StringToHash("Attack4");


        #endregion

        public enum DragonStates
        {
            Idle, Chase, Attack, Dead
        }

        public enum DragonPhases
        {
            First = 1, Second, Third
        }

        public enum DragonAttacks
        {
            Basic, Claw, Flyflame, Flame
        }

        private void Awake()
        {
            anim = GetComponent<Animator>();
            navAgent = GetComponent<NavMeshAgent>();
            controller = GetComponent<EnemyController>();
            player = FindFirstObjectByType<PlayerController>();

            DragonIdleState enemyIdleState = new(DragonStates.Idle, this);
            DragonChaseState enemyChaseState = new(DragonStates.Chase, this);
            DragonAttackState enemyAttackState = new(DragonStates.Attack, this);
            DragonDeadState enemyDeadState = new(DragonStates.Dead, this);

            states.Add(DragonStates.Idle, enemyIdleState);
            states.Add(DragonStates.Chase, enemyChaseState);
            states.Add(DragonStates.Attack, enemyAttackState);
            states.Add(DragonStates.Dead, enemyDeadState);

            currentState = states[DragonStates.Idle];

            attacksAvailable = new Dictionary<int, DragonAttacks>
            {
                { (int) DragonAttacks.Basic, DragonAttacks.Basic },
                { (int) DragonAttacks.Claw, DragonAttacks.Claw }
            };

            coldownAttacks = new Dictionary<DragonAttacks, float>
            {
                { DragonAttacks.Basic, 1.05f + coldownTimeAttack },
                { DragonAttacks.Claw, 3f + coldownTimeAttack },
                { DragonAttacks.Flame, 2.2f + coldownTimeAttack },
                { DragonAttacks.Flyflame, 4f + coldownTimeAttack }
            };

            UpdateNextAttack();
        }

        private void FixedUpdate()
        {
            float distanceEnemyPlayer = Vector3.Distance(player.transform.position, transform.position);

            playerInRangeToChase = distanceEnemyPlayer < distanceToChasePlayer;
            playerInRangeToAttack = distanceEnemyPlayer < nextAttackDistance;

            anim.SetFloat(animSpeed, navAgent.velocity.sqrMagnitude);
        }

        private void CalculateDistanceToNextAttack()
        {
            switch (nextAttack)
            {
                case DragonAttacks.Basic:
                    nextAttackDistance = distanceToBasicAttack;
                    break;
                case DragonAttacks.Claw:
                    nextAttackDistance = distanceToClawAttack;
                    break;
                case DragonAttacks.Flyflame:
                    nextAttackDistance = distanceToFlyFlameAttack;
                    break;
                case DragonAttacks.Flame:
                    nextAttackDistance = distanceToFlameAttack;
                    break;
            }

            navAgent.stoppingDistance = nextAttackDistance;
        }

        public void UpdateNextAttack()
        {
            int[] keys = attacksAvailable.Keys.ToArray();
            int nextAttackInt = Random.Range(nextAttackStartIndex, attacksAvailable.Count);
            nextAttack = attacksAvailable[keys[nextAttackInt]];
            CalculateDistanceToNextAttack();
        }

        public IEnumerator SecondPhase()
        {
            isTransitioning = !isTransitioning;
            var lastSpeed = navAgent.speed;
            navAgent.speed = 0;
            anim.SetBool(animTransitioning, isTransitioning);
            anim.SetTrigger(animScream);
            currentPhase = DragonPhases.Second;

            yield return new WaitForSeconds(3.1f);

            navAgent.speed = lastSpeed;
            isTransitioning = !isTransitioning;
            anim.SetBool(animTransitioning, isTransitioning);
            anim.SetBool(animFlying, true);
            nextAttackStartIndex = 2;

            attacksAvailable.Add((int)DragonAttacks.Flyflame, DragonAttacks.Flyflame);
            UpdateNextAttack();
        }

        public IEnumerator ThirdPhase()
        {
            isTransitioning = !isTransitioning;
            var lastSpeed = navAgent.speed;
            navAgent.speed = 0;
            anim.SetBool(animTransitioning, isTransitioning);
            anim.SetBool(animFlying, false);
            transform.DORotate(Vector3.zero, 1);
            currentPhase = DragonPhases.Third;

            yield return new WaitForSeconds(3.2f);

            navAgent.speed = lastSpeed;
            isTransitioning = !isTransitioning;
            anim.SetBool(animTransitioning, isTransitioning);
            nextAttackStartIndex = 0;

            attacksAvailable.Remove((int)DragonAttacks.Flyflame);
            attacksAvailable.Add((int)DragonAttacks.Flame, DragonAttacks.Flame);
            UpdateNextAttack();
        }
    }
}
