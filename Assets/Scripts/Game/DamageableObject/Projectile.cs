using DigitalRuby.PyroParticles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TokioSchool.FinalProject.Enemy;
using TokioSchool.FinalProject.Player;
using UnityEngine;
using UnityEngine.Events;

namespace TokioSchool.FinalProject.Damageables
{
    public class Projectile : Damageable
    {
        public UnityAction<GameObject, Collision> onCollide;

        private float timeAliveReset;
        private bool dead = false;


        private void Start()
        {
            timeAliveReset = DamageableObject.TimeAlive;
        }

        private void Update()
        {
            if (timeAliveReset < 0 && !dead)
            {
                Death();
            }

            timeAliveReset -= Time.deltaTime;
        }

        private void FixedUpdate()
        {
            rb.velocity = transform.forward * DamageableObject.Speed;
        }

        protected override void HandleDamage(Collision collision)
        {
            base.HandleDamage(collision);

            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if (player == null)
            {
                onCollide?.Invoke(collision.gameObject, collision);
                if (DamageableObject.HasAreaDamage)
                {
                    CreateExplosion(collision.contacts[0].point, DamageableObject.AreaRadius, DamageableObject.AreaForce);
                }
            }
        }

        private void CreateExplosion(Vector3 pos, float radius, float force)
        {
            if (force <= 0.0f || radius <= 0.0f)
            {
                return;
            }

            // find all colliders and add explosive force
            Collider[] objects = Physics.OverlapSphere(pos, radius);
            foreach (Collider col in objects)
            {
                Rigidbody r = col.GetComponent<Rigidbody>();
                EnemyController enemy = col.gameObject.GetComponent<EnemyController>();
                if (enemy != null && !enemy.Dead)
                {
                    enemy.HandleHit(DamageableObject.Damage);
                }
                if (r != null)
                {
                    r.AddExplosionForce(force, pos, radius);
                }
            }
        }
    }
}