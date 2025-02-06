using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
	[SerializeField] private AudioClip clickClips;
	[SerializeField] private GameObject howToPlayPanel;

	private void Awake()
	{
		
		howToPlayPanel.SetActive(false);
	}

	public void ActivePanel()
	{
		SoundFXManager.instance.PlaySoundFXClip(clickClips, transform, 1f);
		howToPlayPanel.SetActive(true);
	}

	public void ClosePanel()
	{
		SoundFXManager.instance.PlaySoundFXClip(clickClips, transform, 1f);
		howToPlayPanel.SetActive(false);
	}

	public void SceneChange(string name)
	{
		SceneManager.LoadScene(name);
		Time.timeScale = 1.0f;
		SoundFXManager.instance.PlaySoundFXClip(clickClips, transform, 1f);
	}

	public void ExitFromGamee()
	{
		SoundFXManager.instance.PlaySoundFXClip(clickClips, transform, 1f);
		Application.Quit();
	}

}
