using KBCore.Refs;
using UnityEngine;

namespace Utils
{
    public class MainCamera : ValidatedMonoBehaviour
    {
        public static Camera Current { get; private set; }

        [SerializeField, Self] private Camera thisCamera;

        private void Awake()
        {
            Current = thisCamera;
        }
    }
}