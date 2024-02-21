using System;
using System.Collections;
using System.Collections.Generic;
using TokioSchool.FinalProject.Equipments;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TokioSchool.FinalProject.UI
{
    public class EquipmentSlot : MonoBehaviour
    {
        [SerializeField] private Weapon weapon;
        [SerializeField] private InputAction inputAction;

        private UIPlayerController uiPlayerController;

        private void Awake()
        {
            uiPlayerController = GetComponentInParent<UIPlayerController>();
            inputAction.performed += OnActionPerformed;
        }

        private void OnEnable()
        {
            inputAction.Enable();
        }

        private void OnDestroy()
        {
            inputAction.Disable();
        }

        private void OnActionPerformed(InputAction.CallbackContext obj)
        {
            uiPlayerController.SetupWeaponUI(weapon);
        }
    }
}
