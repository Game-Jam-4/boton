using UnityEngine;

public class AttackCard : Card {
    public override void PlayCard(CharacterComponent character)
    {
        character.Attack();
    }
}
