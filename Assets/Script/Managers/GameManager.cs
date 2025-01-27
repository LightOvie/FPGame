using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEditor.Experimental.GraphView.GraphView;
using System;
public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public GameStatus currentState;
	public GameStatus previousState;

	[SerializeField]
	TMP_Text stopWatchDisplay;
	[SerializeField]
	TMP_Text deadEnimiesText;

	[Header("Screens")]
	//public GameObject pauseScreen;
	public GameObject resultScren;
	public GameObject pauseScreen;

	[Header("Result Screen Display")]
	public TMP_Text timeSurviveDisplay;
	public TMP_Text totalDeadEnimies;


	[HideInInspector]
	public bool isGameOver { get { return currentState == GameStatus.GameOver; } }
	[HideInInspector]
	public bool isPaused { get { return currentState == GameStatus.PausedGame; } }

	private int counterDeadEnimes;
	private float stopwatchTime;
	GunData gunData;



	private void Awake()
	{
		instance = this;
		gunData = ScriptableObject.CreateInstance<GunData>(); //Will be used later
		DisableScreen();
	}




	// Update is called once per frame
	void Update()
	{

		deadEnimiesText.text = string.Format("Enimies: " + counterDeadEnimes.ToString());




		switch (currentState)
		{
			case GameStatus.InProgress:
				CheckForPausedAndResume();
				UpdateStopWatch();
				break;
			case GameStatus.GameOver:
				break;
			case GameStatus.PausedGame:
				CheckForPausedAndResume();
				break;
			default:
				Debug.LogWarning("STATE DOES NOT EXIST");
				break;
		}
	}

	public void PauseGame()
	{
		if (currentState!=GameStatus.PausedGame)
		{
			ChangeState(GameStatus.PausedGame);
			Time.timeScale = 0f;
			Cursor.lockState = CursorLockMode.None;
			pauseScreen.SetActive(true);
		}
	}
	public void ResumeGame()
	{
		if (currentState == GameStatus.PausedGame)
		{
			ChangeState(previousState);
			Time.timeScale = 1f;
			Cursor.lockState = CursorLockMode.Locked;
			pauseScreen.SetActive(false);
		}
	}

	private void CheckForPausedAndResume()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (currentState==GameStatus.PausedGame)
			{
				ResumeGame();
			}
            else
            {
				PauseGame();
            }
        }
	}

	public void InccreadeDeadEnimies()
	{

		counterDeadEnimes++;
	}

	void UpdateStopWatch()
	{
		stopwatchTime += Time.deltaTime;
		UpdateStopWatchDisplay();
	
	}

	void UpdateStopWatchDisplay()
	{
		int minutes = Mathf.FloorToInt(stopwatchTime / 60);
		int second = Mathf.FloorToInt(stopwatchTime % 60);

		stopWatchDisplay.text = string.Format("{0:00}:{1:00}", minutes, second);
	}

	public void ChangeState(GameStatus newState)
	{
		previousState = currentState;

		currentState = newState;
	}


	public void GameOver()
	{
		timeSurviveDisplay.text=stopWatchDisplay.text;
		totalDeadEnimies.text =counterDeadEnimes.ToString();
		
		ChangeState(GameStatus.GameOver);
		Time.timeScale = 0f;
		DisplayResult();
		
	}

	void DisableScreen()
	{
		resultScren.SetActive(false);
		pauseScreen.SetActive(false);
	}



	private void DisplayResult()
	{
		resultScren.SetActive(true);
		Cursor.lockState = CursorLockMode.Confined;
	}
}
