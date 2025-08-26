using KBCore.Refs;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
    public class ToolTipManager : MonoBehaviour
    {
        public static ToolTipManager Instance { get; private set; }
        
        [SerializeField, Anywhere] private TMP_Text title;
        [SerializeField, Anywhere] private TMP_Text body;
        [SerializeField, Anywhere] private CanvasGroup canvasGroup;

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

        public void Show(string titleText, string bodyText)
        {
            title.text = titleText;
            body.text = bodyText;
            Tween.Alpha(canvasGroup, 1, 0.1f);
        }

        public void Hide()
        {
            Tween.Alpha(canvasGroup, 0, 0.1f);
        }
    }
}