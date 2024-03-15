using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapProceduralGenerator : MonoBehaviour
{
    [SerializeField] private MapNode NodePrefab;
    [SerializeField] private int RowsNumber;
    [SerializeField] private int MaxNodes;
    [SerializeField] private int InitialX;
    [SerializeField] private int InitialY;
    [SerializeField] private int YOffset;

    private readonly List<GameObject> _nodes = new();
    private List<int> _nodesCount;

    public static Action<List<GameObject>, List<int>> OnMapGenerated;

    private void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        _nodesCount = new List<int>(RowsNumber);
        int currentY = InitialY, count = 0;
        const int xOffset = 4;

        for (int i = 0; i < RowsNumber; i++)
        {
            if (i == RowsNumber - 1)
            {
                CreateNode(InitialX, currentY).SetRow(i);
                _nodesCount.Add(1);
                continue;
            }
            
            if (i != 0)
            {
                int nodesNumber = Random.Range(2, MaxNodes + 1);
                int currentX = nodesNumber == 2 ? -nodesNumber : -nodesNumber - (nodesNumber - 2);
                currentX += InitialX;
                
                for (int j = 0; j < nodesNumber; j++)
                {
                    CreateNode(currentX, currentY).SetRow(i);
                    currentX += xOffset;
                    count++;
                }

                _nodesCount.Add(count);
                count = 0;
                currentY += YOffset;
                continue;
            }

            CreateNode(InitialX, InitialY).SetRow(i);
            _nodesCount.Add(1);
            currentY += YOffset;
        }
        
        OnMapGenerated?.Invoke(_nodes, _nodesCount);
    }

    private MapNode CreateNode(int x, int y)
    {
        GameObject node = Instantiate(NodePrefab.gameObject, new Vector3(x, y), Quaternion.identity, transform);
        _nodes.Add(node);

        return node.GetComponent<MapNode>();
    }

    public void ClearNodes()
    {
        if (_nodes.Count <= 0) return;
        
        _nodes.ForEach(Destroy);
        _nodes.Clear();
    }
}
