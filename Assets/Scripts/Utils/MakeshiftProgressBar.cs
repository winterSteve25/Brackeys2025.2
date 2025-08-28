using KBCore.Refs;
using UnityEngine;

namespace Utils
{
    public class MakeshiftProgressBar : ValidatedMonoBehaviour
    {
        [SerializeField, Self] private RectTransform rect;
        [SerializeField, Child(Flag.ExcludeSelf)] private RectTransform progressFill;

        private float _max = 1;

        public void Initialize(float max)
        {
            _max = max;
        }

        public void UpdateValue(float val)
        {
            var delta = progressFill.sizeDelta;
            delta.x = Mathf.Clamp01(val / _max) * rect.sizeDelta.x;
            progressFill.sizeDelta = delta;
        }
    }
}