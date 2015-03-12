using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TunSegment : MonoBehaviour {

	public struct Ring {
		// Where the center of bottom ring should be on the XZ plane
		public float centerX, centerY, centerZ, radius;
		public Ring(float centerX, float  centerY, float centerZ, float radius) {
			this.centerX = centerX;
			this.centerY = centerY;
			this.centerZ = centerZ;
			this.radius = radius;
		}
		public Ring(Vector3 center, float radius) {
			this.centerX = center.x;
			this.centerY = center.y;
			this.centerZ = center.z;
			this.radius = radius;
		}
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Creates a Tun Segment between the two rings with n sides.
	public void CreateMesh(Ring top, Ring bottom, int n, Material material) {
		MeshFilter meshFilter = gameObject.AddComponent<MeshFilter> ();
		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
		
		Vector3[] vertices = new Vector3[n*2];
		List<Vector3> hardVertices = new List<Vector3>();
		Vector2[] uv = new Vector2[n*2*3];
		int[] triangles = new int[n*2*3];
		for(int i = 0; i < n; i++)
		{
			vertices[i+n] = new Vector3(
				bottom.radius * Mathf.Cos(2 * Mathf.PI * i / n) + bottom.centerX,
				bottom.centerY,
				bottom.radius * Mathf.Sin(2 * Mathf.PI * i / n) + bottom.centerZ
				);

			vertices[i] = new Vector3(
				top.radius * Mathf.Cos(2 * Mathf.PI * i / n) + top.centerX,
				top.centerY,
				top.radius * Mathf.Sin(2 * Mathf.PI * i / n) + top.centerZ
				);
			
			triangles[i*n] = (i + 1) % n;
			triangles[i*n+1] = ((i + n) % n) + n;
			triangles[i*n+2] = i;
			
			triangles[i*n+3] = (i + 1) % n;
			triangles[i*n+4] = ((i + n + 1) % n) + n;
			triangles[i*n+5] = ((i + n) % n) + n;
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
		
		Mesh m = meshFilter.mesh;
		m.Clear();
		m.vertices = hardVertices.ToArray();
		m.uv = uv;
		m.triangles = triangles;
		m.RecalculateBounds();
		m.RecalculateNormals();

		meshRenderer.material = material;

//		return m;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
