using System;
using UnityEngine;

public class ClassSelectorConfirmButton : MonoBehaviour
{
    private ClassSelector _selector;

    private void Awake()
    {
        _selector = GetComponentInParent<ClassSelector>();
    }

    public void OnConfirmButton()
    {
        Character current = _selector.GetCurrentClass();
        CharacterManager.Instance.AddCharacter(new PlayableCharacter(current));
        GameManager.Instance.ResumeGame();
        
        SceneManagement.Instance.LoadScene(SceneIndexes.Age1);
    }
}
