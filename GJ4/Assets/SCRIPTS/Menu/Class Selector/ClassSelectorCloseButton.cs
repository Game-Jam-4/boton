using System;
using UnityEngine;

public class ClassSelectorCloseButton : MonoBehaviour {
    public Action OnCloseButton;

    public void OnClose()
    {
        OnCloseButton?.Invoke();
    }
}
