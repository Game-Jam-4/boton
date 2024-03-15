using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text Name;
    [SerializeField] private TMP_Text AttackText;
    [SerializeField] private TMP_Text DefenseText;
    [SerializeField] private TMP_Text SpeedText;
    [SerializeField] private bool IsEnemy;

    private void OnEnable()
    {
        if (!IsEnemy) CombatManager.OnCharacterSelected += SetStats;
        else CombatManager.OnEnemySelected += SetStats;
    }

    private void OnDisable()
    {
        if (!IsEnemy) CombatManager.OnCharacterSelected -= SetStats;
        else CombatManager.OnEnemySelected -= SetStats;
    }

    private void SetStats(CharacterComponent playableCharacter)
    {
        CultureInfo info = CultureInfo.InvariantCulture;
        PlayableCharacter charClass = playableCharacter.CharacterStats();
        
        SetName(charClass.Class().Name);
        SetStats(charClass.GetStat(Stats.Ataque).ToString(info),
            charClass.GetStat(Stats.Defensa).ToString(info),
            charClass.GetStat(Stats.Velocidad).ToString(info));
    }

    private void SetStats(Enemy enemy)
    {
        CultureInfo info = CultureInfo.InvariantCulture;
        
        SetName(enemy.Info().Name);
        SetStats(enemy.Stat(Stats.Ataque).Value.ToString(info),
            enemy.Stat(Stats.Defensa).Value.ToString(info),
            enemy.Stat(Stats.Velocidad).Value.ToString(info));
    }

    private void SetName(string charName) => Name.SetText(charName);

    private void SetStats(string atk, string def, string speed)
    {
        AttackText.SetText($"<color=red>Ataque:</color> {atk}");
        DefenseText.SetText($"<color=blue>Defensa:</color> {def}");
        SpeedText.SetText($"<color=green>Velocidad:</color> {speed}");
    }
}
