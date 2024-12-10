using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEditor.Experimental.GraphView.GraphView;
public class GameManager : MonoBehaviour
{

	public static GameManager instance;
	//Counter dead animes 
	//Status of the game 
	//Timer 
	[SerializeField]
	TMP_Text stopWatchDisplay;

	[SerializeField]
	TMP_Text deadEnimiesText;

	private int counterDeadEnimes;
	private float stopwatchTime;

	
	private void Awake()
	{ 
		instance = this;
	}


	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		//UpdateStopWatch(); do it later add text in Canvas for timer
		deadEnimiesText.text=string.Format("Enimies: "+ counterDeadEnimes.ToString());
	}
	public void InccreadeDeadEnimies() {

		counterDeadEnimes++;
	}

	void UpdateStopWatch()
	{
		stopwatchTime += Time.deltaTime;
		UpdateStopWatchDisplay();
		//if (stopwatchTime >= timeLimit)
		//{

		//}
	}

	void UpdateStopWatchDisplay()
	{
		int minutes = Mathf.FloorToInt(stopwatchTime / 60);
		int second = Mathf.FloorToInt(stopwatchTime % 60);

		stopWatchDisplay.text = string.Format("{0:00}:{1:00}", minutes, second);
	}
}
