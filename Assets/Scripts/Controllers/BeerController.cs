using UnityEngine;
using System.Collections;

public class BeerController : MonoBehaviour {

	private GameObject cup;
	private GameObject ball;
	private BallController ballController;
	private float cupRemovalDelay = 1.0f;
	private float triggerTime = 0.0f;

	// Use this for initialization
	void Start () {
		cup = gameObject.transform.parent.gameObject;
		ball = GameObject.FindGameObjectWithTag ("Player");
		ballController = ball.GetComponent<BallController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if ((triggerTime > 0.0f) && (Time.time - triggerTime > cupRemovalDelay)) {
			RemoveCup ();
		}
	}

	void OnTriggerEnter() {
		triggerTime = Time.time;
	}

	void RemoveCup() {
		cup.SetActive (false);
		GameManager.instance.EndThrow (true);
	}

}
