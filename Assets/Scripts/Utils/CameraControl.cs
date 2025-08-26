using KBCore.Refs;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Utils
{
    public class CameraControl : ValidatedMonoBehaviour
    {
        [SerializeField, Self] private CinemachineFollow composer;
        [SerializeField] private InputActionReference movement;
        [SerializeField] private float minTilt;
        [SerializeField] private float maxTilt;
        [SerializeField] private float lerpParam;
        
        private void Update()
        {
            var dir = movement.action.ReadValue<Vector2>();
            var percentage = (dir.y + 1) / 2;
            composer.FollowOffset.y = Mathf.Lerp(composer.FollowOffset.y, Mathf.Lerp(maxTilt, minTilt, percentage), lerpParam);
        }
    }
}