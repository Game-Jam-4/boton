using System;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    private ClassSelector _classSelector;
    
    private void Awake()
    {
        _classSelector = FindObjectOfType<ClassSelector>();
    }

    public void OnPlayButton()
    {
        _classSelector.Show();
    }
}
