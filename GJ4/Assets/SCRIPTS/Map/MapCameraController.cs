using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class MapCameraController : MonoBehaviour
{
    [SerializeField] private float XInitial;
    [SerializeField] private float YInitial;
    [SerializeField] private float OrthographicSize; 

    private readonly List<MapNode> _nodes = new();
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        _camera.transform.position = new Vector3(XInitial, YInitial, -10);
        _camera.orthographicSize = OrthographicSize;
    }

    private void OnEnable()
    {
        MapProceduralGenerator.OnMapGenerated += OnMapGenerated;
        _nodes.ForEach(node => node.OnNodeClick += HandleCameraMovement);
    }

    private void OnDisable()
    {
        MapProceduralGenerator.OnMapGenerated -= OnMapGenerated;
        _nodes.ForEach(node => node.OnNodeClick -= HandleCameraMovement);
    }
    
    private void OnMapGenerated(List<GameObject> nodes, List<int> rows)
    {
        nodes.ForEach(node => _nodes.Add(node.GetComponent<MapNode>()));
        _nodes.ForEach(node => node.OnNodeClick += HandleCameraMovement);
    }

    private void HandleCameraMovement(MapNode node, int row)
    {
        _camera.transform.DOMoveY(_camera.transform.position.y + Math.Abs(YInitial), 1f);
    }
    
}
