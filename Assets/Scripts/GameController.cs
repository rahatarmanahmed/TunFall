using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static bool gameOver;
	public static int score;

	GameObject player;

	void Awake() {
		score = 0;
		gameOver = false;
	}

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!gameOver)
			score = (int) -player.transform.position.y;
	}

	public void TryAgain() {
		Application.LoadLevel("Main");
	}
}
