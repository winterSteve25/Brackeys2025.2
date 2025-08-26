using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerOxygenMeter : MonoBehaviour
    {
        [SerializeField] private float oxygen;
        [SerializeField] private float oxygenDecreaseRate;
        [SerializeField] private TMP_Text oxygenText;

        private void Update()
        {
            oxygen -= oxygenDecreaseRate * Time.deltaTime;
            oxygenText.text = "Ox: " + oxygen.ToString("P1");
        }
    }
}