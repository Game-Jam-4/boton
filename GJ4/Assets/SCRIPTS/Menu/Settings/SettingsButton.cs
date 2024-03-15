using System;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    private Settings _settings;

    private void Awake()
    {
        _settings = FindObjectOfType<Settings>();
    }

    public void OnSettingsButton()
    {
        _settings.Open();
    }
}
