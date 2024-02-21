using System;
using UnityEngine;

namespace TokioSchool.FinalProject.Projectiles
{
    [CreateAssetMenu(menuName = ("Projectile"))]
    public class ProjectileObject : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] string id;
        [SerializeField] string title;
        [SerializeField] int speed;
        [SerializeField] float damage;
        [SerializeField] float timeAlive;
        [SerializeField] bool isEnemy;
        [SerializeField] GameObject prefab;
        [SerializeField] ParticleSystem particleOnStart;
        [SerializeField] ParticleSystem particleOnEnd;

        public string Id { get => id; }
        public string Name { get => name; }
        public int Speed { get => speed; }
        public float Damage { get => damage; }
        public bool IsEnemy { get => isEnemy; }
        public GameObject Prefab { get => prefab; }
        public ParticleSystem ParticleOnStart { get => particleOnStart; }
        public ParticleSystem ParticleOnEnd { get => particleOnEnd; }
        public float TimeAlive { get => timeAlive; }

        public void OnAfterDeserialize()
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                id = Guid.NewGuid().ToString();
            }
        }

        public void OnBeforeSerialize()
        {
        }
    }
}