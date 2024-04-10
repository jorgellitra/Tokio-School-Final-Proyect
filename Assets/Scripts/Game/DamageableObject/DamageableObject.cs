using System;
using UnityEngine;

namespace TokioSchool.FinalProject.Damageables
{
    [CreateAssetMenu(menuName = ("DamageableObject"))]
    public class DamageableObject : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] string id;
        [SerializeField] string title;
        [SerializeField] int speed;
        [SerializeField] float damage;
        [SerializeField] float timeAlive;
        [SerializeField] GameObject prefab;
        [SerializeField] ParticleSystem particleOnStart;
        [SerializeField] ParticleSystem particleOnEnd;
        [SerializeField] bool isProyectile;
        [SerializeField] bool hasAreaDamage;
        [SerializeField] float areaRadius;
        [SerializeField] float areaForce;

        public string Id { get => id; }
        public string Name { get => name; }
        public int Speed { get => speed; }
        public float Damage { get => damage; }
        public GameObject Prefab { get => prefab; }
        public ParticleSystem ParticleOnStart { get => particleOnStart; }
        public ParticleSystem ParticleOnEnd { get => particleOnEnd; }
        public float TimeAlive { get => timeAlive; }
        public bool IsProyectile { get => isProyectile; }
        public bool HasAreaDamage { get => hasAreaDamage; }
        public float AreaRadius { get => areaRadius; }
        public float AreaForce { get => areaForce; }

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