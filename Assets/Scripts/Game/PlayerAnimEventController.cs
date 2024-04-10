using System.Collections;
using System.Collections.Generic;
using TokioSchool.FinalProject.Equipments;
using UnityEngine;

namespace TokioSchool.FinalProject.Player
{
    public class PlayerAnimEventController : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip onDead;
        [SerializeField] private AudioClip onHurt;
        [SerializeField] private AudioClip onRun;
        [SerializeField] private AudioClip onJump;
        [SerializeField] private AudioClip onHit;
        [SerializeField] private AudioClip onStep;

        private Equipment equipment;

        private void Awake()
        {
            equipment = GetComponentInParent<Equipment>();
        }

        public void OnJump()
        {
            PlayEffect(onJump);
        }

        public void OnHit()
        {
            PlayEffect(onHit);
        }

        public void OnStep()
        {
            PlayEffect(onStep);
        }

        public void OnHurt()
        {
            PlayEffect(onHurt);
        }

        public void OnRun()
        {
            PlayEffect(onRun);
        }

        public void OnDead()
        {
            PlayEffect(onDead);
        }

        public void OnActionAnimStart()
        {
            PlayEffect(equipment.CurrentWeapon.ActionAudioStart);
        }

        public void OnActionAnimEnd()
        {
            PlayEffect(equipment.CurrentWeapon.ActionAudioEnd);
        }

        public void OnHoldAnimStart()
        {
            PlayEffect(equipment.CurrentWeapon.HoldAudioStart);
        }

        public void OnHoldAnimEnd()
        {
            PlayEffect(equipment.CurrentWeapon.HoldAudioEnd);
        }

        public void PlayEffect(AudioClip clip)
        {
            if (clip == null)
            {
                return;
            }
            if (audioSource.isPlaying)
            {
                audioSource.PlayOneShot(clip);
            }
            else
            {
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }
}
