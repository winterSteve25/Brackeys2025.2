using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Utils
{
    public class CameraEffects : MonoBehaviour
    {
        public static CameraEffects Current { get; private set; }
        public float trauma = 0;

        [SerializeField] private CinemachineRecomposer rotController;
        [SerializeField] private CinemachineFollow posController;
        [SerializeField] private float maxShakeAngle = 10;
        [SerializeField] private float maxShakeOffset = 40;
        [SerializeField] private float freqMultiplierAngle = 10;
        [SerializeField] private float freqMultiplier = 10;
        [SerializeField] private Volume volume;
        private float _originalVignetteIntensity;

        private void Awake()
        {
            Current = this;
        }

        private void Start()
        {
            if (volume.profile.TryGet(out Vignette vignette))
            {
                _originalVignetteIntensity = vignette.intensity.value;
            }
        }

        public void Update()
        {
            var dt = Time.deltaTime;
            var traumaClamped = Mathf.Clamp01(trauma - dt * 0.55f);
            var shake = traumaClamped * traumaClamped;

            var angle = maxShakeAngle * shake *
                        (Mathf.PerlinNoise(1000, Time.timeSinceLevelLoad * freqMultiplierAngle) * 2 - 1);
            var offX = maxShakeOffset * shake *
                       (Mathf.PerlinNoise(2000, Time.timeSinceLevelLoad * freqMultiplier) * 2 - 1);
            var offY = maxShakeOffset * shake *
                       (Mathf.PerlinNoise(3000, Time.timeSinceLevelLoad * freqMultiplier) * 2 - 1);

            posController.FollowOffset = new Vector3(offX, offY, -10);
            rotController.Dutch = angle;
            trauma = traumaClamped;
        }
    }
}