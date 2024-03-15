using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Game/Character")] 
public class Character : ScriptableObject {
    public string Name;
    public Sprite Icon;
    public List<Stat> InitialStats = new();
    public float InitialHp;
    
    private void OnValidate()
    {
        if (InitialStats.Count > 0) return;
        
        for (int i = 0; i < Enum.GetNames(typeof(Stats)).Length; i++) 
            InitialStats.Add(new Stat((Stats) i));
    }
    
    public Stat GetStat(Stats stat) => InitialStats.Find(x => x.Name == stat);
}
