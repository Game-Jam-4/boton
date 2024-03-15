using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyInteraction : MonoBehaviour, IPointerDownHandler
{
    private Enemy _enemyInfo;
    private bool _selected;

    private void Awake()
    {
        _enemyInfo = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        CombatManager.OnEnemiesGenerated += Initialize;
        CombatManager.OnEnemySelected += OnEnemySelect;
    }

    private void OnDisable()
    {
        CombatManager.OnEnemiesGenerated -= Initialize;
        CombatManager.OnEnemySelected -= OnEnemySelect;
    }

    private void Initialize(List<Character> enemies)
    {
        if (_enemyInfo.GetIndex() >= enemies.Count) return;
        
        if (_enemyInfo.GetIndex() == 0) Select();
    }

    private void OnEnemySelect(Attacker enemy)
    {
        if (enemy == _enemyInfo) return;
        
        Deselect();
    }

    private void Deselect()
    {
        _selected = false;
    }

    private void Select()
    {
        _selected = true;
        CombatManager.Instance.SelectEnemy(_enemyInfo);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_selected || GameManager.Instance.IsPaused()) return;

        Select();
    }
}
