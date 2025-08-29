using UnityEngine;

namespace Objects.UpgradeStation
{
    public abstract class Upgrade : ScriptableObject
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public bool CanOnlyAppearOnce { get; private set; }
        [field: SerializeField] public int AppearAfterDay { get; private set; }
        [field: SerializeField] public int Cost { get; private set; }
        
        public abstract string Description { get; }
        public abstract void Apply();
    }
}