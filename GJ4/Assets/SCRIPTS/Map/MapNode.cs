using System;
using DG.Tweening;
using UnityEngine;

public class MapNode : MonoBehaviour
{
    [SerializeField] private Color CurrentColor;
    [SerializeField] private Color UnableColor;
    [SerializeField] private Color VisitedColor;
    [SerializeField] private Color AvailableColor;

    private int _row;
    private bool _canVisit;
    private SpriteRenderer _renderer;
    private const float _COLOR_FADE = 0.5f;

    public Action<MapNode, int> OnNodeClick;

    private void OnEnable()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnDisable()
    {
        _renderer.DOKill();
    }

    public int Row() => _row;
    public void SetRow(int row) => _row = row;

    public void GoToNode()
    {
        _renderer.DOColor(CurrentColor, _COLOR_FADE);
        _canVisit = false;
    }

    public void SetVisited()
    {
        _renderer.DOColor(VisitedColor, _COLOR_FADE);
        _canVisit = false;
    }

    public void SetAvailable()
    {
        _renderer.DOColor(AvailableColor, _COLOR_FADE);
        _canVisit = true;
    }

    public void SetUnable()
    {
        _renderer.DOColor(UnableColor, _COLOR_FADE);
        _canVisit = false;
    }

    public void OnMouseDown()
    {
        if (!_canVisit || GameManager.Instance.OnCombat()) return;
        
        OnNodeClick?.Invoke(this, _row);
        CombatManager.Instance.StartCombat();
    }

    public void OnMouseEnter()
    {
        if (!_canVisit) return;
        
        _renderer.DOFade(0.5f, 0.2f);
    }

    private void OnMouseExit()
    {
        if (!_canVisit) return;
        
        _renderer.DOFade(1f, 0.2f);
    }
}
