using UnityEngine;
using System.Collections;

public class TunCamera : MonoBehaviour {

	public float cameraDistance = 10;

	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		var y = player.transform.position.y + cameraDistance;

		var tun = GameObject.FindWithTag ("Tun");
		var segments = tun.GetComponentsInChildren<TunSegment> (false);
		foreach (var segment in segments) {
			if(segment.IncludesDepth(y)) {
				transform.position = segment.LerpDepth(y);
			}
		}
	}
}
