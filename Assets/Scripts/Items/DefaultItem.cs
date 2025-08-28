using UnityEngine;

namespace Items
{
    public class DefaultItem : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer itemRenderer;

        public void Initialize(Sprite icon)
        {
            itemRenderer.sprite = icon;
        }
    }
}