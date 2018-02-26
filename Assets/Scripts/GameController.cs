using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public float speed;
	public Rigidbody playerRigidBody;
	public GameObject playerCamera;

	// Use this for initialization
	void Start () {
		Debug.Log ("Game controller loaded");
	}

	// Update is called once per frame
	void Update () {
		//Bluetooth controller
		float horizontalInput = Input.GetAxis("Vertical");
		float verticalInput = - Input.GetAxis("Horizontal");

		//Keyboard
//		float horizontalInput = Input.GetAxis("Horizontal");
//		float verticalInput = Input.GetAxis("Vertical");

		//Returns an angle from 0 to 360 degrees (CW), then convert it to rads
		float yAngle = Mathf.Deg2Rad * playerCamera.transform.eulerAngles.y;

		float xVelocity = speed * (verticalInput * Mathf.Sin(yAngle) + horizontalInput * Mathf.Sin(yAngle + Mathf.PI/2));
		float zVelocity = speed * (verticalInput * Mathf.Cos(yAngle) + horizontalInput * Mathf.Cos(yAngle + Mathf.PI/2));

		playerRigidBody.velocity = new Vector3 (
			xVelocity,
			0.0f,
			zVelocity
		);

		//Check if flying buttons are pressed
		fly();

	}

	private void fly() {
		float xAngle = Mathf.Deg2Rad * playerCamera.transform.eulerAngles.x;
		float yAngle = Mathf.Deg2Rad * playerCamera.transform.eulerAngles.y;

		Vector3 velocityVector = new Vector3 (
			Mathf.Sin(yAngle),
			-Mathf.Sin(xAngle),
			Mathf.Cos(yAngle)
		);

		// Debug.Log("Velocity Vector: "+velocityVector);

		if (Input.GetButton("Fire3")) {
			playerRigidBody.velocity = velocityVector * speed;
		} else if (Input.GetButton("Fire2")) {
			playerRigidBody.velocity = velocityVector * speed * -1.0f;
		}

	}
}
