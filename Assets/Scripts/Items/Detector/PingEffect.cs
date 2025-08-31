using UnityEngine;

namespace Items.Detector
{
    public class PingEffect : MonoBehaviour
    {
        [SerializeField] private float growSpeed;

        private float _radius;

        public void Initialize(float radius)
        {
            _radius = radius;
        }

        private void Update()
        {
            transform.localScale += new Vector3(1, 1, 0) * (growSpeed * Time.deltaTime);

            if (transform.localScale.x >= _radius)
            {
                Destroy(gameObject);
            }
        }
    }
}