using KBCore.Refs;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Utils
{
    public class CameraControl : ValidatedMonoBehaviour
    {
        [SerializeField, Self] private CinemachineFollow composer;
        
        private void Update()
        {
            // var dir = movement.action.ReadValue<Vector2>();
            // if (EventSystem.current.IsPointerOverGameObject())
            // {
            //     dir = Vector2.zero;
            // }
            //
            // var percentage = (dir.y + 1) / 2;
            // var from = composer.FollowOffset.y;
            // var target = Mathf.Lerp(maxTilt, minTilt, percentage);
            // if (Mathf.Abs(from - target) < 0.05) return;
            // composer.FollowOffset.y = Mathf.Lerp(from, target, lerpParam);
        }
    }
}