public class Enemy : Attacker
{
    private void OnEnable()
    {
        CombatManager.OnEnemiesGenerated += Initialize;
    }

    private void OnDisable()
    {
        CombatManager.OnEnemiesGenerated -= Initialize;
    }

    public override void Attack()
    {
        float dmg = Info().GetStat(Stats.Ataque).Value - _target.Info().GetStat(Stats.Defensa).Value * 0.2f;
        if (dmg < 0) dmg = 0;
        
        _target.TakeDamage(dmg);
    }
}
