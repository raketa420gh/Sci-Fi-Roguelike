using System;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    private Animator _animator;
    
    public bool IsFading { get; private set; }

    private Action _fadedInCallback;
    private Action _fadedOutCallback;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start() => 
        DontDestroyOnLoad(gameObject);

    public void FadeIn(Action fadedInCallback)
    {
        if (IsFading)
            return;

        IsFading = true;
        _fadedInCallback = fadedInCallback;
        _animator.SetBool(AnimationParameterNames.Faded, true);
    }
    
    public void FadeOut(Action fadedOutCallback)
    {
        if (IsFading)
            return;

        IsFading = true;
        _fadedOutCallback = fadedOutCallback;
        _animator.SetBool(AnimationParameterNames.Faded, false);
    }

    private void HandleFadeInOver()
    {
        _fadedInCallback?.Invoke();
        _fadedInCallback = null;
        IsFading = false;
    }
    
    private void HandleFadeOutOver()
    {
        _fadedOutCallback?.Invoke();
        _fadedOutCallback = null;
        IsFading = false;
    }
}