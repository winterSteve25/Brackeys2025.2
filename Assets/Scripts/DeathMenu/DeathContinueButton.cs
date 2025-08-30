using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DeathMenu
{
    public class DeathContinueButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image image;
        [SerializeField] private Sprite nonHoverSprite;
        [SerializeField] private Sprite hoverSprite;

        public void OnPointerClick(PointerEventData eventData)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Space Ship");
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            image.sprite = hoverSprite;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            image.sprite = nonHoverSprite;
        }
    }
}