using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn_Manager : MonoBehaviour {

	public const int TOTAL_TURNS = 20;
	public float TURN_TIME = 20f;

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


	void Start () {
		startTurns();
		StartCoroutine("turn");
	}

	IEnumerator turn() {
        if (currentTurn < TOTAL_TURNS){
            yield return new WaitForSeconds(TURN_TIME);
            switchTurn();
        }
        else{
            OnTurnSystemFinished();
        }
	}

	void startTurns() {
		currentTurn = 1;

		players[0] = new Player(1, new HumanBehaviour());
		players[1] = new Player(2, new CpuBehaviour());

		activatePlayer(players[0]);

		OnTurnSystemStarted();
	}

	void activatePlayer(Player player) {
		try { currentPlayer.deactivate(); } catch { }
		currentPlayer = player;
        OnPlayerTurn(currentTurn);
        OnTurnStarted();
        currentPlayer.activate();
		//Debug.Log("Turn " + currentTurn + ", Player " + currentPlayer.id + " at " + Time.time + ".");
	}

    //Lo puse publico para poder usarlo desde el onClick de los botones
	public void switchTurn() {

        //Esto evita que al pasar de TOTAL_TURNS, al accionar los botones siga intentano cambiar de turno
        if (currentTurn >= TOTAL_TURNS){
            return;
        }
        StopCoroutine("turn");//Se para la corrutina para que no funcione unicamente en loop y se pueda cancelar al hundir un botón

        currentTurn++;
		if (currentPlayer == players[0]) {
			activatePlayer(players[1]);
		}
		else {
			activatePlayer(players[0]);
		}

		OnTurnChanged(currentTurn);

        StartCoroutine("turn"); //Se vuelve a llamar la corrutina para que no funcione unicamente en loop y se pueda cancelar al hundir un botón
    }

    public void stopCoroutine()
    {
        StopCoroutine("turn");//Se para la corrutina para que no funcione unicamente en loop y se pueda cancelar al hundir un botón
    }
}
