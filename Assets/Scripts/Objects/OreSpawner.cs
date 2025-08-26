using UnityEngine;
using World;

namespace Objects
{
    public class OreSpawner : MonoBehaviour
    {
        [SerializeField] private WorldTile[] prefabs;
        [SerializeField] private float chance;

        private void Start()
        {
            if (Random.value < chance)
            {
                WorldManager.Current.SetTile(transform.position,
                    Instantiate(prefabs[Random.Range(0, prefabs.Length)]));
            }
            else
            {
                WorldManager.Current.RemoveTile(transform.position);
            }
        }
    }
}