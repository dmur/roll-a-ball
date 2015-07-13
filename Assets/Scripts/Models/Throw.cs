using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System; //This allows the IComparable Interface

public class Throw: IComparable<Throw> {

	public readonly bool scored;
	public readonly Turn turn;

	public Throw (bool _scored, Turn _turn) {
		scored = _scored;
		turn = _turn;
	}

	public int CompareTo(Throw other) {
		if (other == null) { return 1; }
		return scored && other.scored ? 0 : 1;
	}
}

