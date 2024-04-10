using TokioSchool.FinalProject.Core;
using TokioSchool.FinalProject.Player;
using UnityEngine;
using UnityEngine.AI;

namespace TokioSchool.FinalProject.Enemy
{
    public class EnemyAttackState : BaseState<EnemyStateMachine.EnemyState>
    {
        private EnemyStateMachine enemy;
        private Animator anim;
        private NavMeshAgent navAgent;
        private PlayerController player;

        private bool isAttacking = false;
        private float attackColdownReset;

        public EnemyAttackState(EnemyStateMachine.EnemyState key, EnemyStateMachine enemy) :
            base(key, enemy)
        {
            this.enemy = enemy;
            this.anim = enemy.Anim;
            this.navAgent = enemy.NavAgent;
            this.player = enemy.Player;
        }

        public override void EnterState()
        {
            navAgent.speed = 0;
            int indexAttack = Random.Range(1, 3);
            anim.SetTrigger("Attack" + indexAttack);
            enemy.ChangeStatusWeaponColliders(true);
            isAttacking = true;
            attackColdownReset = 2;
        }

        public override void ExitState()
        {
            enemy.ChangeStatusWeaponColliders(false);
            isAttacking = false;
            navAgent.speed = enemy.walkSpeed;
        }

        public override EnemyStateMachine.EnemyState GetNextState()
        {
            if (enemy.Controller.Hitted)
            {
                return EnemyStateMachine.EnemyState.Hit;
            }
            if (enemy.Controller.Dead)
            {
                return EnemyStateMachine.EnemyState.Dead;
            }

            return !isAttacking ? EnemyStateMachine.EnemyState.Chase : EnemyStateMachine.EnemyState.Attack;
        }

        public override void UpdateState()
        {
            attackColdownReset -= Time.deltaTime;
            if (attackColdownReset < 1)
            {
                Vector3 direction = player.transform.position - enemy.transform.position;
                enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, Quaternion.LookRotation(direction), 5 * Time.deltaTime);
            }

            isAttacking = attackColdownReset > 0;
        }

        public override void OnTriggerEnter(Collider other)
        {
        }

        public override void OnTriggerExit(Collider other)
        {
        }

        public override void OnTriggerStay(Collider other)
        {
        }
    }
}
