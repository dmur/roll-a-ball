using UnityEngine;
using System.Collections;
using System; //This allows the IComparable Interface

public class Player: IComparable<Player> {

	public int score;
	public string name;
	public int gamesWon;
	public int gamesLost;

	public Player(string _name) {
		name = _name;
	}

	// Comparison method, required by IComparable interface.
	public int CompareTo(Player other) {
		if(other == null) {
			return 1;
		}
		
		//Return the difference in score.
		return score - other.score;
	}

}
