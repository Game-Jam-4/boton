using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClassSelector : MonoBehaviour
{
    private List<Character> _classes;
    private Character _initialClass;

    private List<ClassSelectorArrow> _arrows;
    private ClassSelectorCharacter _character;
    private ClassSelectorText _text;
    private ClassSelectorStats _stats;
    private ClassSelectorCloseButton _close;

    private int _currentIdx;

    private GameObject _viewport;

    private void Awake()
    {
        _arrows = GetComponentsInChildren<ClassSelectorArrow>().ToList();
        _character = GetComponentInChildren<ClassSelectorCharacter>();
        _text = GetComponentInChildren<ClassSelectorText>();
        _stats = GetComponentInChildren<ClassSelectorStats>();

        _close = GetComponentInChildren<ClassSelectorCloseButton>();

        _viewport = transform.GetChild(0).gameObject;
        _viewport.SetActive(false);
    }

    private void OnEnable()
    {
        if (_close) _close.OnCloseButton += Hide;
        _arrows.ForEach(x => x.OnArrowPress += OnCharacterChange);
    }

    private void OnDisable()
    {
        if (_close) _close.OnCloseButton -= Hide;
        _arrows.ForEach(x => x.OnArrowPress -= OnCharacterChange);
    }
    
    public Character GetCurrentClass() => _classes[_currentIdx];
    public void RemoveClass(Character classRemove) => _classes.Remove(classRemove);

    private void OnCharacterChange(int direction)
    {
        if (_currentIdx + direction < 0) _currentIdx = _classes.Count - 1;
        else if (_currentIdx + direction >= _classes.Count) _currentIdx = 0;
        else _currentIdx += direction;

        UpdateCharacter(_classes[_currentIdx]);
    }

    private void UpdateCharacter(Character character)
    {
        _character.SetCharacter(character.Icon);
        _text.SetText(character.Name);
        _stats.SetStats(character.InitialStats);
    }

    public void Hide() => _viewport.SetActive(false);
    public void Show() 
    {
        _classes = Resources.LoadAll<Character>("Classes").ToList();
        
        _currentIdx = 0;
        _initialClass = _classes[_currentIdx];
        UpdateCharacter(_initialClass);
        
        _viewport.SetActive(true);
    }
    
    
}
