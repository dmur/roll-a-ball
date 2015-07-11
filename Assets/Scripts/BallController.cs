using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	public float trackingSpeed;
	public float launchSensitivity;
	public float minPositionY;

	private Rigidbody rb;
	private Vector3 lastMousePos;
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
			lastMousePos = Input.mousePosition;
			launchStartTime = Time.time;
			cameraController.isLockedToPlayer = false;
		}

		if (Input.GetMouseButtonUp (0) && !rb.useGravity) {
			rb.useGravity = true;
			float deltaT = (Time.time - launchStartTime);
			float launchForceSideways = (Input.mousePosition.x - lastMousePos.x)/deltaT * launchSensitivity;
			float launchForceForward = (Input.mousePosition.y - lastMousePos.y)/deltaT * launchSensitivity;
			rb.AddForce (-launchForceForward, 0.0f, launchForceSideways);
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
		gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.useGravity = false;
		cameraController.Reset ();
	}

}
