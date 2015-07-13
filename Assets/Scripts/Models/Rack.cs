using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System; //This allows the IComparable Interface

public class Rack: IComparable<Rack> {

	public readonly GameObject gameObject;
	public Player player;

	public List<GameObject> cups {
		get {
			List<GameObject> _cups = new List<GameObject>();
			foreach (Transform child in gameObject.transform) {
				_cups.Add (child.gameObject);
			}
			return _cups;
		}
	}

	public Rack(GameObject _gameObject, Player _player) {
		gameObject = _gameObject;
		player = _player;
	}

	public int CompareTo(Rack other) {
		if (other == null) {
			return 1;
		}
		return cups.Count - other.cups.Count;
	}

}
