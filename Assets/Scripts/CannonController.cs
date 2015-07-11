using UnityEngine;
using System.Collections;

public class CannonController : MonoBehaviour {

	public float power = 1.0f;
	public float fireRate = 1.0f;
	public float ballLifespan = 1.0f;

	public GameObject ballProjectile;
	public GameObject ballEmitter;
	private Rigidbody ballRB;


	// Use this for initialization
	void Start () {


	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// FixedUpdate is called before pphysics calcs
	void FixedUpdate () {

		if (Input.GetMouseButtonDown (0)) {


		}
		
	}

	// Update is called once per frame
	void Fire () {
		GameObject ball = Instantiate (ballProjectile, ballEmitter.transform.position, Quaternion.identity) as GameObject;
		ballRB.AddForce (0.0f, 1.0f, 0.0f);
	}

}
