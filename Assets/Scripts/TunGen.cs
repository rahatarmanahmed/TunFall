using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TunGen : MonoBehaviour {

	private MeshFilter meshFilter;
	private MeshRenderer meshRenderer;

	public int numTunSides = 6;
	public int segmentLength = 10;
	public int radius = 5;

	// Use this for initialization
	void Start () {
		meshFilter = GetComponent<MeshFilter>();
		meshRenderer = GetComponent<MeshRenderer>();

		InitMesh(meshFilter.mesh);
	}

	Mesh InitMesh(Mesh m) {
		var time = Time.realtimeSinceStartup;
		Vector3[] vertices = new Vector3[numTunSides*2];
		List<Vector3> hardVertices = new List<Vector3>();
		Vector2[] uv = new Vector2[numTunSides*2*3];
		int[] triangles = new int[numTunSides*2*3];
		for(int i = 0; i < numTunSides; i++)
		{
			vertices[i] = new Vector3(
				radius * Mathf.Cos(2 * Mathf.PI * i / numTunSides),
				0,
				radius * Mathf.Sin(2 * Mathf.PI * i / numTunSides)             
			);

			vertices[i+numTunSides] = new Vector3(
				radius * Mathf.Cos(2 * Mathf.PI * i / numTunSides),
				segmentLength,
				radius * Mathf.Sin(2 * Mathf.PI * i / numTunSides)             
				);

			triangles[i*numTunSides] = (i + 1) % numTunSides;
			triangles[i*numTunSides+1] = ((i + numTunSides) % numTunSides) + numTunSides;
			triangles[i*numTunSides+2] = i;
			
			triangles[i*numTunSides+3] = (i + 1) % numTunSides;
			triangles[i*numTunSides+4] = ((i + numTunSides + 1) % numTunSides) + numTunSides;
			triangles[i*numTunSides+5] = ((i + numTunSides) % numTunSides) + numTunSides;
		}

//		for(int k=0;k<vertices.Length-1;k++)
//		{
//			Debug.DrawLine(vertices[k], vertices[k+1], Color.black, 1000, false);
//		}

		for(int k=0;k<triangles.Length;k++) {
			hardVertices.Add(vertices[triangles[k]]);
			triangles[k] = k;
			uv[k] = Vector2.zero;
		}

		m.Clear();
		m.vertices = hardVertices.ToArray();
		m.uv = uv;
		m.triangles = triangles;
		m.RecalculateBounds();
		m.RecalculateNormals();
		Debug.Log("Time to gen mesh: "+(Time.realtimeSinceStartup - time));
		return m;
	}

	// Update is called once per frame
	void Update () {

	}
}
