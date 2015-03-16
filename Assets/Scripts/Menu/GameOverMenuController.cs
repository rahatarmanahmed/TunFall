using UnityEngine;
using System.Collections;

public class GameOverMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(GameController.gameOver) {
			GetComponent<Canvas>().enabled = true;
		}
		else {
			GetComponent<Canvas>().enabled = false;
		}
	}
}
