using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SettingsResolutionText : MonoBehaviour
{
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    public void SetText(string text) => _text.SetText(text);
}
