using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesImages : MonoBehaviour {
    private List<Image> _images = new();

    private void Awake()
    {
        _images = GetComponentsInChildren<Image>().ToList();
    }

    private void OnEnable()
    {
        CombatManager.OnEnemiesGenerated += InitializeImages;
    }

    private void OnDisable()
    {
        CombatManager.OnEnemiesGenerated -= InitializeImages;
    }

    private void InitializeImages(List<Character> enemies)
    {
        for (int i = 0; i < _images.Count; i++)
        {
            if(i >= enemies.Count)
            {
                _images[i].gameObject.SetActive(false);
                continue;
            }
            
            _images[i].gameObject.SetActive(true);
            _images[i].sprite = enemies[i].Icon;
        }
    }
}
