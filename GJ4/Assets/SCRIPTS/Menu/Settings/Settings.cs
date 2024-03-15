using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
	private GameObject _viewport;
	private SoundSlider[] _sliders;

	#region Unity Methods

	private void Start()
	{
		_sliders = GetComponentsInChildren<SoundSlider>();
		foreach (SoundSlider soundSlider in _sliders)
			soundSlider.Initialize();
		
		_viewport = transform.GetChild(0).gameObject;
		_viewport.SetActive(false);
	}

	#endregion

	#region Methods

	public void Open()
	{
		_viewport.SetActive(true);
	}

	public void Close()
	{
		_viewport.SetActive(false);
	}

	#endregion
}