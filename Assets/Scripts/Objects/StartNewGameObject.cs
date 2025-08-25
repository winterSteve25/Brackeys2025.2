using UnityEngine;
using UnityEngine.SceneManagement;

namespace Objects
{
    public class StartNewGameObject : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene("Game Scene");
        }
    }
}