using System;
using TokioSchool.FinalProject.Damageables;
using UnityEngine;

namespace TokioSchool.FinalProject.Equipments
{
    [CreateAssetMenu(menuName = ("Equipment/Weapon"))]
    public class Weapon : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] string id;
        [SerializeField] float attackCooldown;
        [SerializeField] GameObject leftHandPrefab;
        [SerializeField] GameObject rightHandPrefab;
        [SerializeField] AnimationClip holdAnimation;
        [SerializeField] AnimationClip actionAnimation;
        [SerializeField] AudioClip holdAudioStart;
        [SerializeField] AudioClip holdAudioEnd;
        [SerializeField] AudioClip actionAudioStart;
        [SerializeField] AudioClip actionAudioEnd;
        [SerializeField] ParticleSystem particleOnHold;
        [SerializeField] DamageableObject damageableObjectOnAction;
        [SerializeField] bool canAttackWithoutAim;
        [SerializeField] int numberOfProjectiles;
        [SerializeField] int projectilesPerLoad;
        [SerializeField] float reloadCooldown;

        public string Id { get => id; }
        public GameObject LeftHandPrefab { get => leftHandPrefab; }
        public GameObject RightHandPrefab { get => rightHandPrefab; }
        public AnimationClip HoldAnimation { get => holdAnimation; }
        public AnimationClip ActionAnimation { get => actionAnimation; }
        public bool CanAttackWithoutAim { get => canAttackWithoutAim; }
        public bool HasProjectiles { get => numberOfProjectiles > 0; }
        public DamageableObject DamageableObjectOnAction { get => damageableObjectOnAction; }
        public float AttackCooldown { get => attackCooldown; }
        public int NumberOfProjectiles { get => numberOfProjectiles; }
        public int ProjectilesPerLoad { get => projectilesPerLoad; }
        public float ReloadCooldown { get => reloadCooldown; }
        public AudioClip HoldAudioStart { get => holdAudioStart; }
        public AudioClip HoldAudioEnd { get => holdAudioEnd; }
        public AudioClip ActionAudioStart { get => actionAudioStart; }
        public AudioClip ActionAudioEnd { get => actionAudioEnd; }

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