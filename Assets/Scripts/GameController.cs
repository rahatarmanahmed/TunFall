﻿using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static bool gameOver;
	public static int score, highscore;
	public static bool paused;
	

	GameObject player;

	void Awake() {
		highscore = PlayerPrefs.GetInt ("highscore", 0);
		score = 0;
		gameOver = false;
		paused = false;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!gameOver)
			score = (int) -player.transform.position.y;
		if(score > highscore)
			highscore = score;
		if(Input.GetKeyDown(KeyCode.Escape) || !paused && Input.GetMouseButtonDown(0) || !paused && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			if(paused)
				Resume();
			else
				Pause();
		}
	}

	public static void GameOver() {
		gameOver = true;
		highscore = PlayerPrefs.GetInt ("highscore", 0);
		if(score > highscore) {
			PlayerPrefs.SetInt("highscore", score);
			highscore = score;
		}
	}

	public void TryAgain() {
		Application.LoadLevel("Main");
	}

	public void Pause() {
		if(gameOver)
			return;
		paused = true;
		Time.timeScale = 0;
	}

	public void Resume() {
		paused = false;
		Time.timeScale = 1;
	}

	public void Quit() {
		Time.timeScale = 1;
		Application.LoadLevel("Main Menu");
	}
}
