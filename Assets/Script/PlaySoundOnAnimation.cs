using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnAnimation : MonoBehaviour
{

	[SerializeField]
	AudioClip soundClip;

	public void PlaySound()
	{
		if (soundClip != null)
		{
			SoundFXManager.instance.PlaySoundFXClip(soundClip, transform, 0.25f);
		}
	}
}
