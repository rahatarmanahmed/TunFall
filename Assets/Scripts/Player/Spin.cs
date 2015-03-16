using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {

	public Vector3 spin = Vector3.zero;
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().angularVelocity = spin;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
