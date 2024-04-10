using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TokioSchool.FinalProject.Tweens;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TokioSchool.FinalProject.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] protected float life;
        [SerializeField] protected float shield;
        [SerializeField] protected Canvas canvasUI;
        [SerializeField] protected Slider sliderLife;
        [SerializeField] protected Slider sliderShield;
        [SerializeField] protected Transform damagePopupContainer;
        [SerializeField] protected GameObject damagePopupPrefab;
        [SerializeField] protected float damage = 10f;

        public float currentLife;
        protected float currentShield;

        public bool Dead = false;
        public bool Hitted = false;

        public float Damage { get => damage; }

        private void Start()
        {
            canvasUI.worldCamera = Camera.main;

            currentLife = life;
            sliderLife.maxValue = life;
            sliderLife.value = currentLife;

            currentShield = shield;
            sliderShield.maxValue = shield;
            sliderShield.value = currentShield;
        }

        private void FixedUpdate()
        {
            canvasUI.transform.LookAt(Camera.main.transform.position);
        }

        public virtual void HandleHit(float damage)
        {
            StartCoroutine(HittedCoroutine());
            var damagePopupObject = Instantiate(damagePopupPrefab, damagePopupContainer);
            damagePopupObject.GetComponent<TextMeshProUGUI>().text = damage.ToString();

            TweenManager.Instance.DoSequence(new List<Tween>() {
                damagePopupObject.GetComponent<CanvasGroup>().DOFade(1, .5f),
                damagePopupObject.GetComponent<CanvasGroup>().DOFade(0, 1.5f)
            }, () =>
            {
                Destroy(damagePopupObject);
            });

            var leftoverDamage = currentShield - damage;

            if (leftoverDamage >= 0)
            {
                currentShield = leftoverDamage;
                sliderShield.value = leftoverDamage;
            }

            if (leftoverDamage < 0)
            {
                currentLife += leftoverDamage;
                sliderLife.value += leftoverDamage;
                currentShield = 0;
                sliderShield.value = 0;
            }

            if (currentLife <= 0 && !Dead)
            {
                HandleDeath();
            }
        }

        public virtual void HandleDeath()
        {
            Dead = true;
        }

        IEnumerator HittedCoroutine()
        {
            Hitted = true;
            yield return new WaitForEndOfFrame();
            Hitted = false;
        }
    }
}
