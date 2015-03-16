using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TunGenerator : MonoBehaviour {

	public int numSides = 6;
	public int numSegments = 10;
	public Material material;
	public float curveLength = 100;
	public float curveStepSize = .1f;

	public float firstSegmentLength = 100;
	public float easyRadius = 10;
	public float hardRadius = 7;
	public float easyTurn = 10;
	public float hardTurn = 20;
	public float secondsToHard = 120;

	private float curveT = 0;
	public float radius, turn;
	private double startTime;
	private Ring lastDesiredCenter;
	private Ring nextDesiredCenter;

	// Use this for initialization
	void Start () {
		// Initialize difficulty;
		radius = easyRadius;
		turn = easyTurn;
		startTime = Time.realtimeSinceStartup;
		// Create beginning ring so player doesn't see outside
		var top = new Ring(0, 10, 0, radius);
		var bottom = new Ring(0, -firstSegmentLength, 0, radius);
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
		var turnDir = Random.insideUnitCircle * turn;
		nextDesiredCenter = new Ring(
			lastDesiredCenter.centerX + turnDir.x,
			lastDesiredCenter.centerY - curveLength,
			lastDesiredCenter.centerZ + turnDir.y,
			radius
		);
	}

	// Update is called once per frame
	void Update () {
		float difficultyT = (float)(ElapsedTime() / secondsToHard);
		radius = Mathf.Lerp(easyRadius, hardRadius, difficultyT);
		turn = Mathf.Lerp(easyTurn, hardTurn, difficultyT);
		while(transform.childCount < numSegments)
			CreateNextSegment();
	}

	double ElapsedTime() {
		return Time.realtimeSinceStartup - startTime;
	}
}
