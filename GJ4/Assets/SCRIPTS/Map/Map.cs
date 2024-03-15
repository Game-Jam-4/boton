using System;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private readonly List<MapNode> _nodes = new();
    private List<int> _nodesCount = new();
    private MapNode _currentNode;
    private int _row;
    
    private void OnEnable()
    {
        MapProceduralGenerator.OnMapGenerated += Initialize;
    }

    private void OnDisable()
    {
        MapProceduralGenerator.OnMapGenerated -= Initialize;
    }

    private void Initialize(List<GameObject> nodes, List<int> count)
    {
        nodes.ForEach(n => _nodes.Add(n.GetComponent<MapNode>()));
        _nodes.ForEach(n => n.SetUnable());
        _currentNode = _nodes[0];
        
        _currentNode.GoToNode();
        _nodesCount = count;
        _row = _nodes[0].Row();
        
        _nodes.ForEach(node => node.OnNodeClick += HandleNodeClick);
        
        EnableNextRow();
    }

    private void HandleNodeClick(MapNode node, int row)
    {
        VisitPreviousRow();
        
        _row = row;
        _currentNode = node;
        _currentNode.GoToNode();
        
        UnableCurrentRow();

        if (_row == _nodesCount.Count - 1)
        {
            SceneManagement.Instance.LoadScene(SceneIndexes.MainMenu);
            return;
        }
        
        EnableNextRow();
    }

    private void VisitPreviousRow()
    {
        foreach (MapNode node in _nodes)
        {
            if (node.Row() > _row) break;
            if (node.Row() != _row) continue;
            
            if (node == _currentNode) node.SetVisited();
            else node.SetUnable();
        }
    }

    private void UnableCurrentRow()
    {
        foreach (MapNode node in _nodes)
        {
            if (node.Row() > _row) break;
            if (node.Row() != _row || node == _currentNode) continue;
            
            node.SetUnable();
        }
    }

    private void EnableNextRow()
    {
        foreach (MapNode node in _nodes)
        {
            if (node.Row() > _row + 1) break;
            if (node.Row() != _row + 1) continue;
            
            node.SetAvailable();
        }
    }
}
