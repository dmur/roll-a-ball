using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System; //This allows the IComparable Interface

public class Player: IComparable<Player> {

	public int score;
	public string username;
	public int gamesWon;
	public int gamesLost;
	public float cameraPositionX;
	public float launchDirection;
	public float ballPositionX;
	public List<Match> matches;

	public Player(string _username, float _cameraPositionX, float _launchDirection, float _ballPositionX) {
		username = _username;
		cameraPositionX = _cameraPositionX;
		launchDirection = _launchDirection; 
		ballPositionX = _ballPositionX;
	}

	public int CompareTo(Player other) {
		if (other == null) { return 1; }
		return score - other.score;
	}

}
