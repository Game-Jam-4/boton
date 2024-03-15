using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CharacterImages : MonoBehaviour
{
    private List<Image> _images = new();

    private void Awake()
    {
        _images = GetComponentsInChildren<Image>().ToList();
    }

    private void OnEnable()
    {
        CombatManager.OnCombatStart += InitializeImages;
    }

    private void OnDisable()
    {
        CombatManager.OnCombatStart -= InitializeImages;
    }

    private void InitializeImages()
    {
        for (int i = 0; i < _images.Count; i++)
        {
            if(i >= CharacterManager.Instance.CharactersCount())
            {
                _images[i].gameObject.SetActive(false);
                continue;
            }
            
            _images[i].gameObject.SetActive(true);
            _images[i].sprite = CharacterManager.Instance.GetCharacter(i).Class().Icon;
        }
    }
}
