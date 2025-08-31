using UnityEngine;
using Utils;
using World;

namespace Objects
{
    public class OreSpawner : MonoBehaviour
    {
        [SerializeField] private WorldTile[] prefabs;
        [SerializeField] private float chance;

        private void Start()
        {
            if (Random.value < Mathf.Clamp01(chance * CarryOverDataManager.Instance.oreSpawnRateMultiplier))
            {
                WorldManager.Current.SetTile(transform.position,
                    Instantiate(prefabs[Random.Range(0, prefabs.Length)], WorldManager.Current.transform));
            }
            else
            {
                WorldManager.Current.RemoveTile(transform.position);
            }
        }
    }
}