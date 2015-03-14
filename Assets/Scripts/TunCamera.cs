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
		for (int k=0;k<segments.Length;k++) {
			var segment = segments[k];
			if(segment.IncludesDepth(y)) {
				transform.position = segment.LerpDepth(y);
				if(k+1 >= segments.Length) return;
				float t = (y - segment.top.centerY) / (segment.bottom.centerY - segment.top.centerY);
				var nextSegment = segments[k+1];
				var oldQ = Quaternion.LookRotation(segment.bottom.Center - segment.top.Center, new Vector3(0,0,1));
				var desiredQ = Quaternion.LookRotation(nextSegment.bottom.Center - nextSegment.top.Center, new Vector3(0,0,1));
				transform.rotation = Quaternion.Slerp(oldQ, desiredQ, t);
				return;
			}
		}
	}
}
