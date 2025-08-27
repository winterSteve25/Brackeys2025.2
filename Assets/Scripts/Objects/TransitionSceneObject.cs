using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Objects
{
    public class TransitionSceneObject : MonoBehaviour
    {
        [SerializeField] private string sceneName;
        
        public void StartGame(PlayerInventory _)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}