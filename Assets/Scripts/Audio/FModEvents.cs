using FMODUnity;
using UnityEngine;

namespace Audio
{
    public class FModEvents : MonoBehaviour
    {
        public static FModEvents Instance { get; private set; }
        
        [field: SerializeField] public EventReference DynamiteExplode { get; private set; }
        [field: SerializeField] public EventReference DynamiteTick { get; private set; }
        [field: SerializeField] public EventReference DynamiteThrow { get; private set; }
        [field: SerializeField] public EventReference RopeClimb { get; private set; }
        [field: SerializeField] public EventReference RopeClimbFinish { get; private set; }
        [field: SerializeField] public EventReference RopePlace { get; private set; }
        [field: SerializeField] public EventReference LaserUse { get; private set; }
        [field: SerializeField] public EventReference DectectorPing { get; private set; }
        
        [field: SerializeField] public EventReference PlayerFall { get; private set; }
        [field: SerializeField] public EventReference PlayerWalk { get; private set; }
        
        [field: SerializeField] public EventReference StonePlace { get; private set; }
        [field: SerializeField] public EventReference StoneBreak { get; private set; }
        
        [field: SerializeField] public EventReference TerminalInteract { get; private set; }
        [field: SerializeField] public EventReference TerminalTeleport { get; private set; }
        
        [field: SerializeField] public EventReference UIHover { get; private set; }
        [field: SerializeField] public EventReference UIBuy { get; private set; }
        [field: SerializeField] public EventReference UISell { get; private set; }
        
        [field: SerializeField] public EventReference AmbienceMusic1 { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}