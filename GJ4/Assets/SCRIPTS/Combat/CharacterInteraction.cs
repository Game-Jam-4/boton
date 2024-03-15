using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterInteraction : MonoBehaviour, IPointerDownHandler
{
    private CharacterComponent _playableCharacter;
    private bool _selected;

    private void Awake()
    {
        _playableCharacter = GetComponent<CharacterComponent>();
    }

    private void OnEnable()
    {
        CombatManager.OnCombatStart += Initialize;
        CombatManager.OnCharacterSelected += OnCharacterSelect;
    }

    private void OnDisable()
    {
        CombatManager.OnCombatStart -= Initialize;
        CombatManager.OnCharacterSelected -= OnCharacterSelect;
    }

    private void Initialize()
    {
        if (_playableCharacter.GetIndex() >= CharacterManager.Instance.CharactersCount()) return;
        
        if (_playableCharacter.GetIndex() == 0) Select();
    }

    private void OnCharacterSelect(CharacterComponent playableCharacter)
    {
        if (playableCharacter == _playableCharacter) return;
        
        Deselect();
    }

    private void Deselect()
    {
        _selected = false;
    }

    private void Select()
    {
        _selected = true;
        CombatManager.Instance.SelectCharacter(_playableCharacter);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_selected || GameManager.Instance.IsPaused()) return;

        Select();
    }
}
