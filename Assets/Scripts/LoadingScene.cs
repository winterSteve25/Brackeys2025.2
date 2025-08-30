using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    #if UNITY_ADDRESSABLES_EXIST
       // List of asset references to load if using Addressables
        public List<AssetReference> AssetReferenceBanks = new List<AssetReference>();
    #else
        // List of Banks to load
        [FMODUnity.BankRef]
        public List<string> Banks = new List<string>();
    #endif
    
    private void Awake()
    {
        StartCoroutine(Load());
    }

    private IEnumerator Load()
    {
#if UNITY_ADDRESSABLES_EXIST
        // Iterate all the asset references and start loading their studio banks
        // in the background, including their audio sample data
        foreach (var bank in AssetReferenceBanks)
        {
            FMODUnity.RuntimeManager.LoadBank(bank, true);
        }
#else
        // Iterate all the Studio Banks and start them loading in the background
        // including the audio sample data
        foreach (var bank in Banks)
        {
            FMODUnity.RuntimeManager.LoadBank(bank, true);
        }
#endif

        // Keep yielding the co-routine until all the bank loading is done
        // (for platforms with asynchronous bank loading)
        while (!FMODUnity.RuntimeManager.HaveAllBanksLoaded)
        {
            yield return null;
        }

        // Keep yielding the co-routine until all the sample data loading is done
        while (FMODUnity.RuntimeManager.AnySampleDataLoading())
        {
            yield return null;
        }

        Debug.Log("Done Loading");
        SceneManager.LoadScene("Space Ship");
    }
}