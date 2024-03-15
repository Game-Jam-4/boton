using System;
using UnityEngine;
using UnityEngine.UI;

public class ClassSelectorCharacter : MonoBehaviour
{
    private Image _character;

    public void SetCharacter(Sprite character)
    {
        if (!_character) _character = GetComponent<Image>();
        
        _character.sprite = character;
    }
}
