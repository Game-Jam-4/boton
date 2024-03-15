using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
	#region Instance

	[Tooltip("Instance of SoundManager, so it can be accessed from other classes.")]
	public static SoundManager Instance;

	#endregion

	#region Private Fields
	
	[SerializeField] private AudioMixerGroup MusicMixerGroup;
	[SerializeField] private AudioMixerGroup SoundEffectsMixerGroup;
	[SerializeField] private List<Sound> Sounds;

	#endregion

	#region Unity Events

	/// <summary>
	/// Initializes Instance. Calls for it to not be destroyed when loading new scenes.
	/// It initializes each sound in _sounds, establishes their type, an plays them if playOnAwake is true.
	/// </summary>
	private void Awake()
	{
		if (Instance == null) Instance = this;
		else
		{
			Destroy(gameObject);
			return;
		}

		foreach (Sound s in Sounds)
		{
			s.Source = gameObject.AddComponent<AudioSource>();
			s.Source.clip = s.AudioClip;
			s.Source.loop = s.IsLoop;
			s.Source.volume = s.Volume;

			switch (s.AudioType)
			{
				case SoundType.SFX:
					s.Source.outputAudioMixerGroup = SoundEffectsMixerGroup;
					break;

				case SoundType.MUSIC:
					s.Source.outputAudioMixerGroup = MusicMixerGroup;
					break;
			}

			if (s.PlayOnAwake)
				s.Source.Play();
		}
	}

	#endregion

	#region Methods

	public void Play(Sound sound)
	{
		Sound s = Sounds.Find(x => x.ClipName == sound.ClipName);
		if (!s) throw new Exception("Not sound found");
		
		s.Source.Play();
	}

	public bool IsPlaying(Sound sound)
	{
		Sound s = Sounds.Find(x => x.ClipName == sound.ClipName);
		if (!s) throw new Exception("Not sound found");

		return s.Source.isPlaying;
	}

	public void Resume(Sound sound)
	{
		Sound s = Sounds.Find(x => x.ClipName == sound.ClipName);
		if (!s) throw new Exception("Not sound found");
			
		s.Source.UnPause();
	}

	public void Pause(Sound sound)
	{
		Sound s = Sounds.Find(x => x.ClipName == sound.ClipName);
		if (!s) throw new Exception("Not sound found");
		
		s.Source.Pause();
	}
	
	public void Stop(Sound sound)
	{
		Sound s = Sounds.Find(x => x.ClipName == sound.ClipName);
		if (s == null) throw new Exception("Not sound found");
		
		s.Source.Stop();
	}

	#endregion

}

