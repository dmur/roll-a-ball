using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	public bool isLockedToPlayer;

	private Vector3 offset;
	private Vector3 homePosition;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
		homePosition = transform.position;
		Reset ();
	}
	
	// LateUpdate is called once per frame, but is guaranteed to run 
	// after all items have been processed in Update()
	void LateUpdate () {
		if (isLockedToPlayer) {
			transform.position = player.transform.position + offset;
		}
		if (Input.GetMouseButtonDown (0)) {
			isLockedToPlayer = false;
		}
	}

	public void Reset () {
		transform.position = homePosition;
		isLockedToPlayer = true;
	}

}
