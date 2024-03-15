using UnityEngine;

public class HealCard : Card
{
    [SerializeField] private int HealingAmount = 5;
    
    public override void PlayCard(CharacterComponent character)
    {
        character.Heal(HealingAmount);
    }
}
