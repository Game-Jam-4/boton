using TMPro;
using UnityEngine;

public class ClassSelectorText : MonoBehaviour
{
    private TMP_Text _text;

    public void SetText(string text)
    {
        if (!_text) _text = GetComponent<TMP_Text>();
        
        _text.SetText(text);
    }
    
}
