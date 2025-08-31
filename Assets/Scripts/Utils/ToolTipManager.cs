using KBCore.Refs;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Utils
{
    public class ToolTipManager : MonoBehaviour
    {
        public static ToolTipManager Instance { get; private set; }
        
        [SerializeField, Anywhere] private TMP_Text title;
        [SerializeField, Anywhere] private TMP_Text body;
        [SerializeField, Anywhere] private CanvasGroup canvasGroup;
        [SerializeField] private Image rightClickIcon;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                return;
            }
            
            Destroy(gameObject);
        }

        private void OnEnable()
        {
            SceneManager.sceneUnloaded += SceneManagerOnsceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneUnloaded -= SceneManagerOnsceneLoaded;
        }

        private void SceneManagerOnsceneLoaded(Scene arg0)
        {
            Hide();
        }

        public void Show(string titleText, string bodyText, bool showRightClick)
        {
            title.text = titleText;
            body.text = bodyText;
            Tween.Alpha(canvasGroup, 1, 0.1f);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform) canvasGroup.transform);

            if (showRightClick)
            {
                rightClickIcon.gameObject.SetActive(true);
            }
            else
            {
                rightClickIcon.gameObject.SetActive(false);
            }
        }

        public void Hide()
        {
            Tween.Alpha(canvasGroup, 0, 0.1f);
        }
    }
}