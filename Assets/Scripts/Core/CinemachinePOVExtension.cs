using Cinemachine;
using UnityEngine;

namespace TokioSchool.FinalProject.Core
{
    public class CinemachinePOVExtension : CinemachineExtension
    {
        [SerializeField] private float horizontalSpeed = 10f;
        [SerializeField] private float verticalSpeed = 10f;
        [SerializeField] private float clampVerticalAngle = 50f;

        private InputManager inputManager;
        private Vector3 startingRotation;

        protected override void Awake()
        {
            inputManager = InputManager.Instance;
            startingRotation = transform.localRotation.eulerAngles;
            base.Awake();
        }

        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (vcam.Follow && inputManager)
            {
                if (stage == CinemachineCore.Stage.Aim)
                {
                    Vector2 deltaInput = inputManager.GetMouseDelta();
                    startingRotation.x += deltaInput.x * verticalSpeed * Time.deltaTime;
                    startingRotation.y += deltaInput.y * horizontalSpeed * Time.deltaTime;
                    startingRotation.y = Mathf.Clamp(startingRotation.y, -clampVerticalAngle, clampVerticalAngle);
                    state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
                }
            }
        }
    }
}
