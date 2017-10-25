using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn_Manager : MonoBehaviour {

	public Player[] players = new Player[2];

	int currentTurn;
	Player currentPlayer;



	void Start () {
		currentTurn = 1;
	
		players[0] = new Player(1, new HumanBehaviour());
		players[1] = new Player(2, new CpuBehaviour());

		currentPlayer = players[0];
		players[0].activate();

	}
	
	void Update () {
		currentPlayer.act();
		Debug.Log(currentPlayer.id);

		if (Input.GetKeyDown(KeyCode.A)){
			if (currentPlayer == players[0]) {
				currentPlayer = players[1];
			}
			else {
				currentPlayer = players[0];
			}
		} 
	}
}
