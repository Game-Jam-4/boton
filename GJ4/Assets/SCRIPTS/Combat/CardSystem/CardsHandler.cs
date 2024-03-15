using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardsHandler : MonoBehaviour
{
    [SerializeField] private List<Card> Cards;
    [SerializeField] private int CardNumber;

    private readonly List<GameObject> _cards = new();

    private void OnEnable()
    {
        CombatManager.OnCombatStart += DrawCards;
        CombatManager.OnNewTurn += DrawCards;
    }

    private void OnDisable()
    {
        CombatManager.OnCombatStart -= DrawCards;
        CombatManager.OnNewTurn -= DrawCards;
    }

    private void DrawCards()
    {
        if (_cards.Count > 0)
        {
            _cards.ForEach(Destroy);
            _cards.Clear();
        }
        
        for (int i = 0; i < CardNumber; i++)
        {
            int type = Random.Range(0, Cards.Count);
            GameObject card = Instantiate(Cards[type].gameObject, transform);
            _cards.Add(card);
        }
    }
}
