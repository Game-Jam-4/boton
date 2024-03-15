using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public GameObject canvas, button, option;
    public void MenuOn()
    {
        canvas.SetActive(true);
        button.SetActive(false);
    }
    public void MenuOff() {
        canvas.SetActive(false);
        button.SetActive(true);
    }
    public void Options()
    {
        option.SetActive(true);
    }
    public void Exit()
    {
        SceneManager.LoadScene("_SCENES/MainMenu");
    }
}
