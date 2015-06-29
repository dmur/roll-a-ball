using UnityEngine;
using System.Collections;

public class GroundController : MonoBehaviour {

	private BallController ballController;

	// Use this for initialization
	void Start () {
		ballController = GameObject.FindGameObjectWithTag ("Player").GetComponent<BallController> ();
	}
	
	// Update is called once per frame
	void OnTriggerEnter () {
		Debug.Log ("triggered");
		ballController.Reset ();
	}
}
