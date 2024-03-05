using System;
using System.Collections;
using System.Collections.Generic;
using TokioSchool.FinalProject.Player;
using UnityEngine;

namespace TokioSchool.FinalProject.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ProjectileObject projectileObject;

        private Rigidbody rb;
        private BoxCollider collider;
        private float timeAliveReset;
        private bool dead = false;

        public ProjectileObject ProjectileObject { get => projectileObject; }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            collider = GetComponent<BoxCollider>();
            timeAliveReset = projectileObject.TimeAlive;
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
            rb.velocity = transform.forward * projectileObject.Speed;
        }

        private void Death()
        {
            dead = true;
            rb.velocity = Vector3.zero;
            if (projectileObject.ParticleOnEnd != null)
            {
                var particleOnEndObject = Instantiate(projectileObject.ParticleOnEnd);
                particleOnEndObject.transform.position = transform.position;
            }
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<PlayerController>() == null)
            {
                Death();
            }
        }
    }
}