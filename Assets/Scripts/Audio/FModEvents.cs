using FMODUnity;
using UnityEngine;

namespace Audio
{
    public class FModEvents : MonoBehaviour
    {
        public static FModEvents Instance { get; private set; }

        [field: SerializeField] public EventReference TerminalInteract { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}