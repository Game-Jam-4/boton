using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SettingsResolution : MonoBehaviour
{
    private Resolution[] _resolutions;
    private readonly List<Resolution> _filteredResolutions = new();
    private RefreshRate _refreshRate;
    private List<SettingsResolutionArrow> _arrows;
    private SettingsResolutionText _text;
    private int _currentIdx;

    private void Awake()
    {
        _arrows = GetComponentsInChildren<SettingsResolutionArrow>().ToList();
        _text = GetComponent<SettingsResolutionText>();
        
        _resolutions = Screen.resolutions;
        _refreshRate = Screen.currentResolution.refreshRateRatio;

        foreach (Resolution resolution in _resolutions)
        {
            if (Mathf.Approximately((float) resolution.refreshRateRatio.value, (float) _refreshRate.value))
                _filteredResolutions.Add(resolution);
        }

        if (PlayerPrefs.HasKey("Resolution"))
        {
            _currentIdx = PlayerPrefs.GetInt("Resolution");    
        }
        else
        {
            for (int i = 0; i < _filteredResolutions.Count; i++)
            {
                if (_filteredResolutions[i].width == Screen.width && _filteredResolutions[i].height == Screen.height)
                    _currentIdx = i;
            }
        }

        Resolution res = _filteredResolutions[_currentIdx];
        Screen.SetResolution(res.width, res.height, PlayerPrefs.GetInt("FullScreen", 1) == 1);
    }

    private void Start()
    {
        _text.SetText($"{_filteredResolutions[_currentIdx].width}x{_filteredResolutions[_currentIdx].height}");
    }

    private void OnEnable()
    {
        _arrows.ForEach(x => x.OnArrowPress += SetResolution);
    }

    private void OnDisable()
    {
        _arrows.ForEach(x => x.OnArrowPress -= SetResolution);
    }

    private void SetResolution(int direction)
    {
        if (_currentIdx + direction < 0) _currentIdx = _filteredResolutions.Count - 1;
        else if (_currentIdx + direction >= _filteredResolutions.Count) _currentIdx = 0;
        else _currentIdx += direction;
        
        Resolution res = _filteredResolutions[_currentIdx];
        
        Screen.SetResolution(res.width, res.height, PlayerPrefs.GetInt("FullScreen", 1) == 1);
        PlayerPrefs.SetInt("Resolution", _currentIdx);
        _text.SetText($"{res.width}x{res.height}");
    }
}
