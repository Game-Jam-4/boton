using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement Instance;

    private void Awake()
    {
        if (!Instance) Instance = this;
    }
    
    private readonly List<AsyncOperation> _sceneAsyncOperation = new();
    private IEnumerator _enumeratorOperations;
    private SoundManager _soundManager;
    private float _totalSceneProgress;
    
    public Action OnStartLoading;
    public Action OnEndLoading;
    public Action<float> OnLoadProgress;
    
    public void LoadScene(SceneIndexes sceneToLoad, SceneIndexes currentScene)
    {
        AddSceneToUnload(currentScene);
        AddSceneToLoad(sceneToLoad);
        UpdateScenes();
    }

    public void LoadScene(SceneIndexes sceneToLoad)
    {
        UnloadActiveScenes();
        AddSceneToLoad(sceneToLoad);
        UpdateScenes();
    }
        
    private void AddSceneToLoad(SceneIndexes scene)
    { 
        _sceneAsyncOperation.Add(SceneManager.LoadSceneAsync((int) scene, LoadSceneMode.Additive));
    }

    private void AddSceneToUnload(SceneIndexes scene)
    {
        _sceneAsyncOperation.Add(SceneManager.UnloadSceneAsync((int) scene));
    }

    private void UnloadActiveScenes()
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            Scene scene = SceneManager.GetSceneByBuildIndex(i);
            if(scene != SceneManager.GetSceneByBuildIndex((int) SceneIndexes.SHARED) && scene.isLoaded)
                AddSceneToUnload((SceneIndexes) scene.buildIndex);
        }
    }

    private void UpdateScenes()
    {
        if (_sceneAsyncOperation.Count <= 0)
            throw new Exception("No scenes to update");
            
        StartCoroutine(GetSceneLoadProgress());
    }
    
    private IEnumerator GetSceneLoadProgress()
    {
        OnStartLoading?.Invoke();
            
        foreach (AsyncOperation operation in _sceneAsyncOperation)
        {
            _totalSceneProgress = 0;
                
            while (!operation.isDone)
            {
                foreach (AsyncOperation op in _sceneAsyncOperation)
                {
                    _totalSceneProgress += op.progress;
                }

                _totalSceneProgress /= _sceneAsyncOperation.Count;
                OnLoadProgress?.Invoke(_totalSceneProgress);
                    
                yield return null;
            }
        }

        if (_enumeratorOperations != null) 
            yield return _enumeratorOperations;
            
        OnEndLoading?.Invoke();
        _sceneAsyncOperation.Clear();
    }
}
