using UnityEngine;

public class DefenseCard : Card
{
    [SerializeField] private int DefenseRaise = 5;

    private CharacterComponent _character;
    
    public override void PlayCard(CharacterComponent character)
    {
        _character = character;
        character.CharacterStats().AddStat(Stats.Defensa, DefenseRaise);
        CombatManager.OnNewTurn += OnTurnEnd;
    }

    private void OnTurnEnd()
    {
        _character.CharacterStats().AddStat(Stats.Defensa, -DefenseRaise);
        CombatManager.OnNewTurn -= OnTurnEnd;
    }
}
