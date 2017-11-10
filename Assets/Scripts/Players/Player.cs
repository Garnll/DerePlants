using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

	public int id;
	public bool onTurn;
	public IBehaviour behaviour;
	public int score;

    public bool controlled = false;

	public Player(int i, IBehaviour b) {
		id = i;
		behaviour = b;
	}

	public void act() {
		behaviour.act();
	}

	public void activate() {
		onTurn = true;
	}

	public void deactivate() {
		onTurn = false;
	}

}
