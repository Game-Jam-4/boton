using System;
using UnityEngine;

public class SettingsResolutionArrow : MonoBehaviour {
    private enum Direction
    {
        Right,
        Left
    }

    [SerializeField] private Direction ArrowDirection;
    public Action<int> OnArrowPress;

    public void OnArrow()
    {
        OnArrowPress?.Invoke(ArrowDirection == Direction.Left ? -1 : 1);
    }
}
