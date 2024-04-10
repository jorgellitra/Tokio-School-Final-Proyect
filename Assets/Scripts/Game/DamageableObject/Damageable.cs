using System.Collections;
using System.Collections.Generic;
using TokioSchool.FinalProject.Enemy;
using TokioSchool.FinalProject.Player;
using Unity.VisualScripting;
using UnityEngine;


namespace TokioSchool.FinalProject.Damageables
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public class Damageable : MonoBehaviour
    {
        [SerializeField] private DamageableObject damageableObject;

        protected Rigidbody rb;
        private bool canDamage = false;

        public DamageableObject DamageableObject { get => damageableObject; }
        public bool CanDamage { get => canDamage; set => canDamage = value; }

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (canDamage)
            {
                HandleDamage(collision);
            }
        }

        protected void Death()
        {
            rb.velocity = Vector3.zero;
            if (damageableObject.ParticleOnEnd != null)
            {
                var particleOnEndObject = Instantiate(damageableObject.ParticleOnEnd);
                particleOnEndObject.transform.position = transform.position;
            }
            if (damageableObject.IsProyectile)
            {
                Destroy(gameObject);
            }
        }

        protected virtual void HandleDamage(Collision collision)
        {
            PlayerController player = collision.gameObject.GetComponentInParent<PlayerController>();
            EnemyController enemy = collision.gameObject.GetComponentInParent<EnemyController>();

            if (player == null)
            {
                if (enemy != null && !enemy.Dead)
                {
                    enemy.HandleHit(damageableObject.Damage);
                }
                Death();
            }
        }
    }
}
