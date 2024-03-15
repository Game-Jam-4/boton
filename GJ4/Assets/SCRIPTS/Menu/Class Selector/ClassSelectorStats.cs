using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ClassSelectorStats : MonoBehaviour
{
    [SerializeField] private string[] Colors = { "red", "blue", "green" };
    
    private List<TMP_Text> _statsTexts = new();

    private void OnEnable()
    {
        if (_statsTexts.Count <= 0) _statsTexts = GetComponentsInChildren<TMP_Text>().ToList();
    }

    public void SetStats(List<Stat> stats)
    {
        int i = 0;
        if (_statsTexts.Count <= 0) _statsTexts = GetComponentsInChildren<TMP_Text>().ToList();
        
        stats.ForEach(stat =>
        {
            _statsTexts[i].SetText($"<b><color=\"{Colors[i]}\">{stat.Name}:</b></color> {stat.Value:00}");
            i++;
        });
    }
}
