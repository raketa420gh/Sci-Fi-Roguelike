using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneLoader
{
    private ScreenFader _screenFader;
    private bool isLoading;

    [Inject]
    public void Construct(ScreenFader screenFader)
    {
        _screenFader = screenFader;
    }

    public void RestartScene(Action onLoaded = null) => 
        LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, onLoaded);
    
    public void LoadScene(string sceneName, Action onLoaded = null) => 
        LoadSceneAsync(sceneName, onLoaded);

    public void LoadNextScene(Action onLoaded = null)
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        var maxSceneIndex = SceneManager.sceneCountInBuildSettings;

        if (currentSceneIndex < maxSceneIndex)
            LoadSceneAsync(currentSceneIndex + 1, onLoaded);
        
        if (currentSceneIndex == maxSceneIndex)
        {
            Debug.Log("Last scene");
            LoadSceneAsync(0, onLoaded);
        }
    }

    private async Task LoadSceneAsync(string sceneName, Action onLoaded = null)
    {
        isLoading = true;
        
        var waitFading = true;
        
        _screenFader.FadeIn(() => waitFading = false);

        while (waitFading)
            await UniTask.Yield();

        var async = SceneManager.LoadSceneAsync(sceneName);
        
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
            await UniTask.Yield();

        async.allowSceneActivation = true;
        
        waitFading = true;
        
        _screenFader.FadeOut(() => waitFading = false);

        while (waitFading)
            await UniTask.Yield();

        isLoading = false;
        
        onLoaded?.Invoke();
    }
    
    private async Task LoadSceneAsync(int sceneIndex, Action onLoaded = null)
    {
        isLoading = true;
        
        var waitFading = true;
        
        _screenFader.FadeIn(() => waitFading = false);

        while (waitFading)
            await UniTask.Yield();

        var async = SceneManager.LoadSceneAsync(sceneIndex);
        
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
            await UniTask.Yield();

        async.allowSceneActivation = true;

        waitFading = true;
        
        _screenFader.FadeOut(() => waitFading = false);

        while (waitFading)
            await UniTask.Yield();

        isLoading = false; 
        
        onLoaded?.Invoke();
    }
}