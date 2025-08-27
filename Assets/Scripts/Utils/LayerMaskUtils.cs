using UnityEngine;

namespace Utils
{
    public class LayerMaskUtils
    {
        public static ContactFilter2D EverythingMask(bool useTriggers)
        {
            var player = 1 << LayerMask.NameToLayer("Player");
            var ignored = 1 << LayerMask.NameToLayer("Ignored");
            var mask = player | ignored;
            
            ContactFilter2D filter = ContactFilter2D.noFilter;
            filter.SetLayerMask(~mask);
            filter.useTriggers = useTriggers;
            
            return filter;
        }
    }
}