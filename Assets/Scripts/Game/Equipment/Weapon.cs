using System;
using TokioSchool.FinalProject.Projectiles;
using UnityEngine;

namespace TokioSchool.FinalProject.Equipments
{
    [CreateAssetMenu(menuName = ("Equipment/Weapon"))]
    public class Weapon : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] string id;
        [SerializeField] string title;
        [SerializeField] GameObject leftHandPrefab;
        [SerializeField] GameObject rightHandPrefab;
        [SerializeField] AnimationClip holdAnimation;
        [SerializeField] AnimationClip actionAnimation;
        [SerializeField] ParticleSystem particleOnHold;
        [SerializeField] ProjectileObject projectileOnAction;
        [SerializeField] bool canAttackWithoutAim;

        public string Id { get => id; }
        public string Name { get => name; }
        public GameObject LeftHandPrefab { get => leftHandPrefab; }
        public GameObject RightHandPrefab { get => rightHandPrefab; }
        public AnimationClip HoldAnimation { get => holdAnimation; }
        public AnimationClip ActionAnimation { get => actionAnimation; }
        public bool CanAttackWithoutAim { get => canAttackWithoutAim; }
        public ProjectileObject ProjectileOnAction { get => projectileOnAction; }

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