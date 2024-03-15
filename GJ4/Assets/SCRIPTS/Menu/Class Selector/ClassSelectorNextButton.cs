using UnityEngine;

public class ClassSelectorNextButton : MonoBehaviour
{
    [SerializeField] private ClassSelector HideSelector;
    [SerializeField] private ClassSelector ShowSelector;

    public void OnNextButton()
    {
        Character current = HideSelector.GetCurrentClass();
        CharacterManager.Instance.AddCharacter(new PlayableCharacter(current));
            
        HideSelector.Hide();
        ShowSelector.Show();
        
        ShowSelector.RemoveClass(current);
    }
}
