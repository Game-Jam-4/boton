using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    [SerializeField] private bool DebugMode;
    private bool _paused;
    private bool _onCombat;
    
    private void Start()
    {
        if (!DebugMode) SceneManagement.Instance.LoadScene(SceneIndexes.MainMenu);
        _paused = !DebugMode;
    }

    public bool IsPaused() => _paused;
    public void PauseGame() => _paused = true;
    public void ResumeGame() => _paused = false;

    public bool OnCombat() => _onCombat;
    public void OnCombatStart() => _onCombat = true;
    public void OnCombatFinish() => _onCombat = false;
}
