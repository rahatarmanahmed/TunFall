using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TunGen : MonoBehaviour {

	public int numSides = 6;
	public int numSegments = 10;
	public float segmentLength = 10;
	public Material material;

	private float currentY = 0;

	// Use this for initialization
	void Start () {
		// Create beginning ring so player doesn't see outside
		var top = new TunSegment.Ring(0, 20, 0, 5);
		var bottom = new TunSegment.Ring(0, 0, 0, 5);
		CreateSegment (top, bottom);
	}

	void CreateSegment(TunSegment.Ring top, TunSegment.Ring bottom) {
		GameObject obj = new GameObject ();
		TunSegment segment = obj.AddComponent<TunSegment>();
		segment.CreateMesh (top, bottom, numSides, material);
		obj.transform.parent = transform;
	}

	void CreateNextSegment()
	{

		var top = new TunSegment.Ring(0, currentY, 0, 5);
		currentY -= segmentLength;
		var bottom = new TunSegment.Ring(0, currentY, 0, 5);
		CreateSegment (top, bottom);
	}

	// Update is called once per frame
	void Update () {
		while(transform.childCount < numSegments)
			CreateNextSegment();
	}
}
