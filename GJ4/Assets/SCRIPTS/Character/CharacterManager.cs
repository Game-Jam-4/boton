using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;

    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    private readonly List<PlayableCharacter> _characters = new ();

    public void AddCharacter(PlayableCharacter playableCharacter) => _characters.Add(playableCharacter);
    public List<PlayableCharacter> Characters() => _characters;
    public void DeleteLastCharacter() => _characters.RemoveAt(_characters.Count - 1);
    public int CharactersCount() => _characters.Count;
    public PlayableCharacter GetCharacter(int idx) => _characters[idx];
}
