using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TokioSchool.FinalProject.Enemy
{
    public class EnemyAnimEventController : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip onAttack1Start;
        [SerializeField] private AudioClip onAttack1End;
        [SerializeField] private AudioClip onAttack2Start;
        [SerializeField] private AudioClip onAttack2End;
        [SerializeField] private AudioClip onDead;
        [SerializeField] private AudioClip onHurt;
        [SerializeField] private AudioClip onIdle;
        [SerializeField] private AudioClip onStep;

        public virtual void OnAttack1Start()
        {
            PlayEffect(onAttack1Start);
        }

        public virtual void OnAttack1End()
        {
            PlayEffect(onAttack1End);
        }

        public virtual void OnAttack2Start()
        {
            PlayEffect(onAttack2Start);
        }

        public virtual void OnAttack2End()
        {
            PlayEffect(onAttack2End);
        }

        public virtual void OnStep()
        {
            PlayEffect(onStep);
        }

        public virtual void OnHurt()
        {
            PlayEffect(onHurt);
        }

        public virtual void OnIdle()
        {
            PlayEffect(onIdle);
        }

        public virtual void OnDead()
        {
            PlayEffect(onDead);
        }

        protected void PlayEffect(AudioClip clip)
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