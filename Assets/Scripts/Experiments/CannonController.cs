using UnityEngine;
using System.Collections;

public class CannonController : MonoBehaviour {

	public float power = 1.0f;
	public float fireRate = 1.0f;
	public float ballLifespan = 1.0f;
	public bool firing = false;
	public int ballsFired = 0;

	public Rigidbody ballProjectile;
	public GameObject ballEmitter;


	// Use this for initialization
	void Start () {


	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// FixedUpdate is called before physics calcs
	void FixedUpdate () {

		if (Input.GetMouseButtonDown (0) && ( !firing )) {
			firing = true;
			InvokeRepeating("Fire", 0.0f, fireRate);

		}
		else if (Input.GetMouseButtonUp (0) && firing ) {
			firing = false;
			CancelInvoke("Fire");
			
		}

		
	}

	// Update is called once per frame
	void Fire () {
		//Rigidbody ball = Instantiate (ballProjectile, ballEmitter.transform.position, Quaternion.identity) as Rigidbody;
		ballsFired ++;
		string thisBall = ballsFired.ToString("0000");
		Rigidbody ball; 
		ball = Instantiate (ballProjectile, ballEmitter.transform.position, Quaternion.identity) as Rigidbody;
		ball.name = ("Ball_" + thisBall );
		ball.AddForce (ballEmitter.transform.forward * power);}

}
