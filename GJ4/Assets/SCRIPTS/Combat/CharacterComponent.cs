using UnityEngine;

public class CharacterComponent : Attacker 
{
    private Card _selectedCard;
    private PlayableCharacter _character;
    
    private void OnEnable()
    {
        CombatManager.OnCharactersInitialized += Initialize;
    }

    private void OnDisable()
    {
        CombatManager.OnCharactersInitialized -= Initialize;
    }
    
    public void SetCard(Card card) => _selectedCard = card;
    public Card CardSelected() => _selectedCard;
    public PlayableCharacter CharacterStats() => _character;
    public void SetCharacterStats(PlayableCharacter character) => _character = character;
    public override void Attack()
    {
        if (!_target) return;
        float dmg = Info().GetStat(Stats.Ataque).Value - _target.Info().GetStat(Stats.Defensa).Value * 0.2f;
        if (dmg < 0) dmg = 0;
        
        _target.TakeDamage(dmg);
    }
}
