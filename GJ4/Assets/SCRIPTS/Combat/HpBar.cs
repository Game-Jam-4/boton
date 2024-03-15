using System;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] private bool IsEnemy;
    private Image _bar;
    private CharacterComponent _character;
    private Enemy _enemy;

    private void Awake()
    {
        _bar = GetComponent<Image>();
        _bar.fillAmount = 1;
    }

    private void OnEnable()
    {
        if (!IsEnemy) CombatManager.OnCharacterSelected += SetCharacter;
        else CombatManager.OnEnemySelected += SetEnemy;
    }

    private void OnDisable()
    {
        if (!IsEnemy) CombatManager.OnCharacterSelected -= SetCharacter;
        else CombatManager.OnEnemySelected -= SetEnemy;
    }

    private void SetCharacter(CharacterComponent character)
    {
        if (_character)
        {
            _character.OnHpChange -= UpdateBar;
        }
        
        _character = character;
        _character.OnHpChange += UpdateBar;
        _bar.fillAmount = _character.GetHp() / _character.GetMaxHp();
    }
    
    private void SetEnemy(Enemy enemy)
    {
        if (_enemy)
        {
            _enemy.OnHpChange -= UpdateBar;
        }
        
        _enemy = enemy;
        _enemy.OnHpChange += UpdateBar;
        _bar.fillAmount = _enemy.GetHp() / _enemy.GetMaxHp();
    }

    private void UpdateBar(Attacker attacker)
    {
        if (!_bar) _bar = GetComponent<Image>();
        
        _bar.fillAmount = attacker.GetHp() / attacker.GetMaxHp();
    }
}
