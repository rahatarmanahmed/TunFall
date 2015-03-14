using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TunGen : MonoBehaviour {

	public int numSides = 6;
	public int numSegments = 10;
	public Material material;
	public float curveLength = 100;
	public float curveStepSize = .1f;
	public float initialRadius = 10;

	private float curveT = 0;
	private Ring lastDesiredCenter;
	private Ring nextDesiredCenter;

	// Use this for initialization
	void Start () {
		// Create beginning ring so player doesn't see outside
		var top = new Ring(0, 20, 0, initialRadius);
		var bottom = new Ring(0, 0, 0, initialRadius);
		CreateSegment (top, bottom);
		nextDesiredCenter = bottom;
		PickNextDesiredCenter();
	}

	void CreateSegment(Ring top, Ring bottom) {
		GameObject obj = new GameObject ();
		TunSegment segment = obj.AddComponent<TunSegment>();
		segment.CreateMesh (top, bottom, numSides, material);
		MeshCollider collider = obj.AddComponent<MeshCollider> ();
		
		obj.transform.parent = transform;
		obj.layer = gameObject.layer;
	}

	void CreateNextSegment() {
		if(curveT >= 1) {
			PickNextDesiredCenter();
			curveT = 0;
		}
		var top = Ring.SmoothStep(lastDesiredCenter, nextDesiredCenter, curveT);
		curveT += curveStepSize;
		var bottom = Ring.SmoothStep(lastDesiredCenter, nextDesiredCenter, curveT);
		CreateSegment (top, bottom);
	}

	void PickNextDesiredCenter() {
		lastDesiredCenter = nextDesiredCenter;
		nextDesiredCenter = new Ring(
			Random.Range(-10, 10),
			lastDesiredCenter.centerY - curveLength,
			Random.Range(-10, 10),
			initialRadius
		);
	}

	// Update is called once per frame
	void Update () {
		while(transform.childCount < numSegments)
			CreateNextSegment();
	}
}
