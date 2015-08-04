using UnityEngine;
using System.Collections;

public class BallManager : MonoBehaviour {

	private Rigidbody rb;
	private Vector3 homePosition;
	private Quaternion homeRotation;

	void Start () {

		rb = GetComponent<Rigidbody> ();
		homePosition = gameObject.transform.position;
		homeRotation = gameObject.transform.rotation;
	}
	
	void FixedUpdate () {
	
		if (Input.GetKey (KeyCode.Alpha1)) {
			Reset ();

		}

	}

	public void Reset () {
		gameObject.transform.position = homePosition;
		gameObject.transform.rotation = homeRotation;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
	}

}
