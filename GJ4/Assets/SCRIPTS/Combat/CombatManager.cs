using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;
    
    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    private List<Character> _enemies;
    private List<Character> _currentEnemies;
    private List<Character> _characters;
    private List<Attacker> _attackers = new();
    private CharacterComponent _selectedPlayableCharacter;
    private Enemy _selectedEnemy;
    
    public static Action OnCombatStart;
    public static Action OnCombatEnd;
    public static Action OnNewTurn;
    public static Action<List<Character>> OnEnemiesGenerated;
    public static Action<List<Character>> OnCharactersInitialized;
    public static Action<CharacterComponent> OnCharacterSelected;
    public static Action<Enemy> OnEnemySelected;
 
    public void SelectCharacter(CharacterComponent playableCharacter)
    {
        if (playableCharacter.CharacterStats() == null)
            playableCharacter.SetCharacterStats(CharacterManager.Instance.GetCharacter(playableCharacter.GetIndex()));
        
        _selectedPlayableCharacter = playableCharacter;
        OnCharacterSelected?.Invoke(playableCharacter);
    }

    public void SelectEnemy(Enemy enemy)
    {
        _selectedEnemy = enemy;
        OnEnemySelected?.Invoke(enemy);
    }

    public void SelectCard(Card card)
    {
        if (_selectedPlayableCharacter.CardSelected()) _selectedPlayableCharacter.CardSelected().Deselect();
        _selectedPlayableCharacter.SetCard(card);
        _selectedPlayableCharacter.SetTarget(_selectedEnemy);
    }
    
    public void StartCombat()
    {
        _enemies = Resources.LoadAll<Character>("Enemies").ToList();
        StartCoroutine(LoadCombatScene());
    }

    private IEnumerator LoadCombatScene()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync((int)SceneIndexes.Combat, LoadSceneMode.Additive);
        GameManager.Instance.OnCombatStart();

        while (!async.isDone)
        {
            yield return null;
        }
        
        GenerateEnemies();
        InitializeCharacters();
        
        OnCombatStart?.Invoke();
    }

    private IEnumerator UnloadCombatScene()
    {
        AsyncOperation async = SceneManager.UnloadSceneAsync((int)SceneIndexes.Combat);
        GameManager.Instance.OnCombatFinish();
        while (!async.isDone)
        {
            yield return null;
        }
        
        OnCombatEnd?.Invoke();
    }

    private void GenerateEnemies()
    {
        int numEnemies = Random.Range(1, 5);
        _currentEnemies = new (numEnemies);

        for (int i = 0; i < numEnemies; i++)
        {
            Character enemy = _enemies[Random.Range(0, _enemies.Count)];
            _currentEnemies.Add(enemy);
        }
        
        OnEnemiesGenerated?.Invoke(_currentEnemies);
        _currentEnemies.Sort((x, y) => y.GetStat(Stats.Velocidad).Value.CompareTo(x.GetStat(Stats.Velocidad).Value));
    }

    private void InitializeCharacters()
    {
        _characters = new();

        for (int i = 0; i < CharacterManager.Instance.CharactersCount(); i++)
            _characters.Add(CharacterManager.Instance.GetCharacter(i).Class());

        OnCharactersInitialized?.Invoke(_characters);
    }

    public void HandleTurn()
    {
        if (_attackers.Count <= 0)
        {
            _attackers = FindObjectsOfType<Attacker>().ToList();
            _attackers.Sort((x, y) => y.Info().GetStat(Stats.Velocidad).Value.CompareTo(x.Info().GetStat(Stats.Velocidad).Value));
        }

        foreach (Attacker attacker in _attackers)
        {
            if (attacker.IsEnemy())
            {
                Attacker target = _attackers.FindAll(x => !x.IsEnemy())[Random.Range(0, _characters.Count)];
                attacker.SetTarget(target);
                attacker.Attack();

                continue;
            }

            CharacterComponent character = attacker as CharacterComponent;
            if (character != null && character.CardSelected()) 
                character.CardSelected().PlayCard(character);
        }

        int enemyCount = 0, characterCount = 0;
        foreach (Attacker attacker in _attackers)
        {
            if (attacker.IsEnemy() && !attacker.IsDead()) enemyCount++;
            else if (!attacker.IsEnemy() && !attacker.IsDead()) characterCount++;
        }
        
        if (characterCount > 0 && enemyCount > 0) OnNewTurn?.Invoke();
        else StartCoroutine(UnloadCombatScene());
    }
}
