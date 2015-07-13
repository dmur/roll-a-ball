using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	public float trackingSpeed;
	public float launchSensitivity;
	public float playerLaunchDirection = 1.0f;
	public Vector3 homePosition;

	// The ball is considered out of bounds if it goes below this position.
	public float minPositionY;

	private Rigidbody rb;
	private Vector3 lastMousePosition;
	private float launchStartTime;
	private CameraController cameraController;
	// Out of bounds reset system w/ delay
	private bool hasMovedOutOfPlay;
	private float timeMovedOutOfPlay;
	private float outOfPlayResetDelay = 2.0f;


	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		homePosition = gameObject.transform.position;
		cameraController = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraController> ();
		Reset ();
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
			lastMousePosition = Input.mousePosition;
			launchStartTime = Time.time;
			cameraController.isLockedToPlayer = false;
		}

		if (Input.GetMouseButtonUp (0) && !rb.useGravity) {
			rb.useGravity = true;
			float deltaT = (Time.time - launchStartTime);
			float launchForceSideways = (Input.mousePosition.x - lastMousePosition.x)/deltaT * launchSensitivity;
			float launchForceForward = (Input.mousePosition.y - lastMousePosition.y)/deltaT * launchSensitivity;
			rb.AddForce (-launchForceForward * playerLaunchDirection, 0.0f, launchForceSideways * playerLaunchDirection);
		}

		if (!hasMovedOutOfPlay && gameObject.transform.position.y < minPositionY) {
			hasMovedOutOfPlay = true;
			timeMovedOutOfPlay = Time.time;
		}

		if (hasMovedOutOfPlay && Time.time > timeMovedOutOfPlay + outOfPlayResetDelay) {
			GameManager.instance.EndThrow ();
		}

		// TODO: Remove in production
		if (Input.GetMouseButtonDown (1)) {
			GameManager.instance.EndThrow ();
		}

	}

	public void Reset () {
		gameObject.transform.position = homePosition;
		gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.useGravity = false;
		timeMovedOutOfPlay = -outOfPlayResetDelay;
		hasMovedOutOfPlay = false;
	}

}
