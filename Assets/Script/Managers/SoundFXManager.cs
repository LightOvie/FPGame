using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{

	public static SoundFXManager instance;

	[SerializeField] private AudioSource soundFXObject;
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	public void PlaySoundFXClip(AudioClip audio, Transform spawnTransform, float volume)
	{
		//spawn in game object
		AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
		//assing the audioClip
		audioSource.clip = audio;
		//assign volume
		audioSource.volume = volume;
		//play sound
		audioSource.Play();
		//get length of sound FX clip
		float clipLength = audioSource.clip.length;
		// Destroy the clip after it is done playing
		Destroy(audioSource.gameObject, clipLength);

	}


	public void PlayRandomSoundFXClip(AudioClip[] audio, Transform spawnTransform, float volume)
	{
		int rand=Random.Range(0, audio.Length);

		//spawn in game object
		AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
		//assing the audioClip
		audioSource.clip = audio[rand];
		//assign volume
		audioSource.volume = volume;
		//play sound
		audioSource.Play();
		//get length of sound FX clip
		float clipLength = audioSource.clip.length;
		// Destroy the clip after it is done playing
		Destroy(audioSource.gameObject, clipLength);

	}
}
