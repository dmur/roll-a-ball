using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	public float trackingSpeed;
	public float launchSensitivity;
	public float minPositionY;

	private Rigidbody rb;
	private float launchForce;
	private float lastMousePosY;
	private float launchStartTime;
	private Vector3 homePosition;
	private CameraController cameraController;


	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		homePosition = gameObject.transform.position;
		cameraController = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraController> ();
	}

	// Called before making any physics calcs
	void FixedUpdate ()
	{
		if (!rb.useGravity) {
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			Vector3 translation = new Vector3 (moveHorizontal, moveVertical, 0.0f);
			gameObject.transform.Translate(translation * trackingSpeed);
		}

		if (Input.GetMouseButtonDown (0) && !rb.useGravity) {
			lastMousePosY = Input.mousePosition.y;
			launchStartTime = Time.time;
			cameraController.isLockedToPlayer = false;
		}

		if (Input.GetMouseButtonUp (0) && !rb.useGravity) {
			rb.useGravity = true;
			launchForce = (Input.mousePosition.y - lastMousePosY)/(Time.time - launchStartTime) * launchSensitivity;
			rb.AddForce (0.0f, 0.0f, launchForce);
		}

		if (gameObject.transform.position.y < minPositionY) {
			Reset ();
		}

		if (Input.GetMouseButtonDown (1)) {
			Reset ();
		}

	}

	public void Reset () {
		gameObject.transform.position = homePosition;
		gameObject.transform.rotation = Quaternion.identity;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.useGravity = false;
		cameraController.Reset ();
	}

}
