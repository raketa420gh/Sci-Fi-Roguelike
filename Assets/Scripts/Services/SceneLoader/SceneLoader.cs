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

    public void RestartScene() => 
        LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    
    public void LoadScene(string sceneName) => 
        LoadSceneAsync(sceneName);

    public void LoadNextScene()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        var maxSceneIndex = SceneManager.sceneCountInBuildSettings;

        if (currentSceneIndex < maxSceneIndex)
            LoadSceneAsync(currentSceneIndex + 1);
        
        if (currentSceneIndex == maxSceneIndex)
        {
            Debug.Log("Last scene");
            LoadSceneAsync(0);
        }
    }

    private async Task LoadSceneAsync(string sceneName)
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
    }
    
    private async Task LoadSceneAsync(int sceneIndex)
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
    }
}