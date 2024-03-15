using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private float _initialY = 999;
    private bool _selected;

    public abstract void PlayCard(CharacterComponent character);
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_selected) return;
        
        transform.DOMoveY(transform.position.y + 50, 0.2f);
        if (_initialY == 999) _initialY = transform.position.y;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_selected) return;
        
        transform.DOMoveY(_initialY, 0.2f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_selected) return;
        
        _selected = true;
        CombatManager.Instance.SelectCard(this);
    }

    public void Deselect()
    {
        _selected = false;
        transform.DOMoveY(_initialY, 0.2f);
    }
}
