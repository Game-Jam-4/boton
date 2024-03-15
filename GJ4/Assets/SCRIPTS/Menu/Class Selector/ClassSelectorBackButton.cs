using System;
using UnityEngine;

public class ClassSelectorBackButton : MonoBehaviour
{
    [SerializeField] private ClassSelector HideSelector;
    [SerializeField] private ClassSelector ShowSelector;

    public void OnBack()
    {
        CharacterManager.Instance.DeleteLastCharacter();
        
        HideSelector.Hide();
        ShowSelector.Show();
    }
}
