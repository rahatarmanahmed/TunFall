using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TunGen : MonoBehaviour {

	public int numTunSides = 6;
	public Material material;

	// Use this for initialization
	void Start () {
		var top = new TunSegment.Ring (Vector3.zero, 5);
		var bottom = new TunSegment.Ring (0, -10, 0, 2);
		CreateSegment (top, bottom);
	}

	void CreateSegment(TunSegment.Ring top, TunSegment.Ring bottom) {
		GameObject obj = new GameObject ();
		TunSegment segment = obj.AddComponent<TunSegment>();
		segment.CreateMesh (bottom, top, numTunSides, material);
		obj.transform.parent = transform;
	}

	// Update is called once per frame
	void Update () {

	}
}
