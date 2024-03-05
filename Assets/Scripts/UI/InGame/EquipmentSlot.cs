using System;
using System.Collections;
using System.Collections.Generic;
using TokioSchool.FinalProject.Equipments;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace TokioSchool.FinalProject.UI
{
    public class EquipmentSlot : MonoBehaviour
    {
        [SerializeField] private Weapon weapon;
        [SerializeField] private InputAction inputAction;
        [SerializeField] private Image bgImage;
        [SerializeField] private Sprite activeImage;
        [SerializeField] private Sprite unActiveImage;

        public Weapon Weapon { get => weapon; }

        private void OnEnable()
        {
            inputAction.Enable();
        }

        private void OnDestroy()
        {
            inputAction.Disable();
        }

        public void ChangeStatus(bool state)
        {
            bgImage.sprite = state ? activeImage : unActiveImage;
        }
    }
}
