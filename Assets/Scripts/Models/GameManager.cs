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
	private int activePlayerIndex;
	
	private int throwsLeftThisTurn;
	private bool scoredFirstCup;

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
			new Player ("player1", 1.411f, 1.0f, -3.2f),
			new Player ("player2", -2.5f, -1.0f, 3.2f)
		};

		StartTurn(0);

		ballController.minPositionY = playSurface.transform.position.y;
	}


	public void StartTurn(int playerIndex) {
		Debug.Log ("Starting turn for player " + playerIndex);
		activePlayer = players[playerIndex];
		activePlayerIndex = playerIndex;
		throwsLeftThisTurn = 2;
		scoredFirstCup = false;
	}


	public void EndTurn(bool scoredLastCup) {
		if (scoredFirstCup && scoredLastCup) {
			StartTurn (activePlayerIndex);
		} else {
			StartTurn ((activePlayerIndex + 1) % players.Count);
			SwitchPlayers (activePlayer);
		}
	}


	// Call this method when the current throw has ended, passing true if the ball was scored.
	public void EndThrow(bool scored = false) {
		throwsLeftThisTurn--;
		ballController.Reset();
		cameraController.Reset();
		if (scored) {
			activePlayer.score++;
			if (throwsLeftThisTurn == 1) scoredFirstCup = true;
			Debug.Log ("Update player score: " + activePlayer.score);
		}
		if (throwsLeftThisTurn == 0) {
			EndTurn (scored);
		}
	}


	public void BounceBack() {
		throwsLeftThisTurn++;
		EndThrow ();
	}


	private void SwitchPlayers(Player newPlayer) {
		activePlayer = newPlayer;
		Vector3 currentCameraPosition = gameCamera.transform.position;
		currentCameraPosition.x = newPlayer.cameraPositionX;
		gameCamera.transform.position = currentCameraPosition;
		gameCamera.transform.Rotate (40, 180, 0);
		ballController.playerLaunchDirection = newPlayer.launchDirection;
		ball.transform.Translate (Vector3.forward * newPlayer.ballPositionX);
		Debug.Log (ball.transform.position);
		ballController.homePosition = ball.transform.position;
	}

}
