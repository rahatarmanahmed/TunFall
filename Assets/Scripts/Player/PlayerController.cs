﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 5;
	public float maxSpeed = 15;
	public float fallSpeed = 50;
	public float dragCoefficient = .25f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(GameController.paused)
			return;
		float hAxis = Input.GetAxis("Horizontal");
		float vAxis = Input.GetAxis("Vertical");
		if(Application.platform == RuntimePlatform.Android
		   || Application.platform == RuntimePlatform.IPhonePlayer) {

			hAxis = Mathf.Clamp (Input.acceleration.x, -1, 1);
			vAxis = Mathf.Clamp (Input.acceleration.y, -1, 1);
		}
		Rigidbody body = gameObject.GetComponent<Rigidbody>();
		body.AddForce(new Vector3(1,0,0)*hAxis*speed);
		body.AddForce(new Vector3(0,0,1)*vAxis*speed);

		var v  = Vector3.ClampMagnitude(new Vector3(body.velocity.x *(1-dragCoefficient), 0, body.velocity.z*(1-dragCoefficient)), maxSpeed);
		v.y = -fallSpeed;
		body.velocity = v;
	}

	void OnCollisionEnter(Collision collision) {
		GameController.GameOver();
	}
	
}

