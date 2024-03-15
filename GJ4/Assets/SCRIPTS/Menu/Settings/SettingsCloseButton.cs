using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SettingsCloseButton : MonoBehaviour
{
	private Settings _settings;

	private void OnEnable()
	{
		if (!_settings) _settings = FindObjectOfType<Settings>();
	}

	public void OnCloseButton()
	{
		_settings.Close();
	}
}
