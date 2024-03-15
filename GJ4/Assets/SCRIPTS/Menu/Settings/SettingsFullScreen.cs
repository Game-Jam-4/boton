using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class SettingsFullScreen : MonoBehaviour
{
    private Toggle _toggle;
    
    private void Awake()
    {
        bool fullScreen = PlayerPrefs.GetInt("FullScreen", 1) == 1;
        _toggle = GetComponent<Toggle>();
        _toggle.isOn = fullScreen;
        Screen.fullScreen = fullScreen;
    }

    public void OnToggleChange(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
        PlayerPrefs.SetInt("FullScreen", fullScreen ? 1 : 0);
    }
}
