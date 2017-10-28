using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn_Manager : MonoBehaviour {

	public const int TOTAL_TURNS = 20;
	public const float TURN_TIME = 4f;

	public Player[] players = new Player[2];
	Player currentPlayer;
	int currentTurn;

	public delegate void TurnManagerEvent();
	public static event TurnManagerEvent OnTurnSystemStarted;
	public static event TurnManagerEvent OnTurnSystemFinished;
	public static event TurnManagerEvent OnTurnStarted;

	public delegate void SpecificTurnEvent(int turn);
	public static event SpecificTurnEvent OnTurnChanged;
	public static event SpecificTurnEvent OnPlayerTurn;


	void Awake() {
	}

	void Start () {
		startTurns();
		StartCoroutine(turn());
	}

	IEnumerator turn() {
		while (currentTurn < TOTAL_TURNS) {
			yield return new WaitForSeconds(TURN_TIME);
			OnPlayerTurn(currentTurn);
			OnTurnStarted();
			switchTurn();
		}

		OnTurnSystemFinished();
	}

	void startTurns() {
		currentTurn = 1;

		players[0] = new Player(1, new HumanBehaviour());
		players[1] = new Player(2, new CpuBehaviour());

		activatePlayer(players[0]);

		OnPlayerTurn(currentTurn);
		OnTurnStarted();
		OnTurnSystemStarted();
	}

	void activatePlayer(Player player) {
		try { currentPlayer.deactivate(); } catch { }
		currentPlayer = player;
		currentPlayer.activate();
		Debug.Log("Turn " + currentTurn + ", Player " + currentPlayer.id + " at " + Time.time + ".");
	}

	void switchTurn() {
		currentTurn++;
		if (currentPlayer == players[0]) {
			activatePlayer(players[1]);
		}
		else {
			activatePlayer(players[0]);
		}

		OnTurnChanged(currentTurn);

	}


}
