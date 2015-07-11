using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject ball;
	public GameObject playSurface;
	public GameObject gameCamera;
	public GameObject player1Rack;
	public GameObject player2Rack;

	
	private BallController ballController;
	private PlaySurfaceController playSurfaceController;
	private CameraController cameraController;

	private List<Player> players;
	private Player activePlayer;

	// Singleton instance of the GameManager
	public static GameManager instance { get; private set; }

	// This is called when the class first loads, before Start()
	void Awake() {
		// Assign the single instance to its static variable.
		instance = this;
	}


	void Start () {
		ballController = ball.GetComponent<BallController> ();
		playSurfaceController = playSurface.GetComponent<PlaySurfaceController> ();
		cameraController = gameCamera.GetComponent<CameraController> ();

		players = new List<Player> () {
			new Player ("player1"),
			new Player ("player2")
		};

		activePlayer = players[0];

		ballController.minPositionY = playSurface.transform.position.y - 0.01f;
	}



	// Update is called once per frame
	void Update () {
		
	}


	public void StartNewGame() {

	}


	// Call this method when the current throw has ended, passing true if the ball was scored.
	public void EndThrow(bool scored = false) {
		ballController.Reset();
		cameraController.Reset();
		if (scored) {
			activePlayer.score++;
			Debug.Log ("Update player score: " + activePlayer.score);
		}
	}
}
