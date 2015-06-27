using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {
	
	public float speed;

	private Rigidbody rb;

	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
	}

	// Called before making any physics calcs
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		if (!rb.useGravity) {
			Vector3 translation = new Vector3 (moveHorizontal, moveVertical, 0.0f);
			gameObject.transform.Translate(translation * speed);
		} else {
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			
			rb.AddForce (movement * speed);
		}


		if (Input.GetMouseButtonDown (0) && !rb.useGravity) {
			rb.useGravity = true;
			rb.AddForce(0.0f, 0.0f, 0.5f);
		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
