using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameInitializer
{
    [RuntimeInitializeOnLoadMethod]
    public static void RunOnStart()
    {
        //if (SceneManager.GetActiveScene().buildIndex != (int)SceneIndexes.SHARED)
         //   SceneManager.LoadSceneAsync((int)SceneIndexes.SHARED, LoadSceneMode.Additive);
    }
}
