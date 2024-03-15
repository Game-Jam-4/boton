using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Attacker : MonoBehaviour {
    [SerializeField] private int Index;
    [SerializeField] private bool Enemy;
    
    private Character _info;
    protected Attacker _target;
    private float _hp;
    private bool _dead;

    [FormerlySerializedAs("OnDamageTake")] public Action<Attacker> OnHpChange;
    public Action<Attacker> OnDie;

    public abstract void Attack();
    
    public void SetTarget(Attacker target) => _target = target;
    public int GetIndex() => Index;
    public Character Info() => _info;
    public bool IsEnemy() => Enemy;
    public bool IsDead() => _dead;
    public float GetHp() => _hp;
    public float GetMaxHp() => _info.InitialHp;

    protected void Initialize(List<Character> characters)
    {
        if (Index >= characters.Count) return;
        
        SetCharacter(characters[Index]);
    }

    private void SetCharacter(Character character)
    {
        _info = character;
        _hp = character.InitialHp;
    }

    public void TakeDamage(float amount)
    {
        _hp -= amount;
        OnHpChange?.Invoke(this);

        if (_hp <= 0) Die();
    }

    public void Heal(float amount)
    {
        _hp += amount;
        OnHpChange?.Invoke(this);
    }

    private void Die()
    {
        _dead = true;
        gameObject.SetActive(false);
        OnDie?.Invoke(this);
    }
    
    public Stat Stat(Stats stat) => _info.GetStat(stat);
}
