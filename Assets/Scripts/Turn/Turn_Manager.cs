using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn_Manager : MonoBehaviour {

	public const int TOTAL_TURNS = 20;
	public float TURN_TIME = 20f;

	//Arreglo con los GameObjects de los jugadores que están en la escena.
	public GameObject[] gameplayers = new GameObject[2];

	//Arreglo con los Players dentro de los GameObjects de los jugadores de la escena.
	public Player[] players = new Player[2];
	
	Player currentPlayer;
	int currentTurn;

	public delegate void TurnManagerEvent();
	public static event TurnManagerEvent OnTurnSystemStarted;
	public static event TurnManagerEvent OnTurnSystemFinished;
	public static event TurnManagerEvent OnTurnStarted;

    public delegate void PlayerHasWonEvent(int playerID, float heightScore);
    public static event PlayerHasWonEvent OnGameFinished;

	public delegate void SpecificTurnEvent(int turn);
	public static event SpecificTurnEvent OnTurnChanged;
	public static event SpecificTurnEvent OnPlayerTurn;

    public delegate void TimedTurnEvent(float turnTime);
    public static event TimedTurnEvent OnTimeStarts;

    void Start () {
		startTurns();
		StartCoroutine("turn");

        Plant.OnPlantMovementEnded += switchTurn;

		for (int i = 0; i < players.Length; i++) {
			Debug.Log(" El jugador #" + (players[i].id) + " es un " + players[i].behaviour.getTypeBehaviour() + ".");
		}
	}

	void startTurns() {
		currentTurn = 1;
		findPlayers();
		activatePlayer(players[0]);
		OnTurnSystemStarted();
	}

	void findPlayers() {
		gameplayers[0] = GameObject.FindWithTag("Player1");
		gameplayers[1] = GameObject.FindWithTag("Player2");
		players = new Player[gameplayers.Length];
		PlayerGameObject[] playerGameObjects = new PlayerGameObject[gameplayers.Length];

		for (int i = 0; i < players.Length; i++) {
			playerGameObjects[i] = gameplayers[i].GetComponent<PlayerGameObject>();
			players[i] = playerGameObjects[i].player;
		}
	}

	IEnumerator turn() {
        if (currentTurn <= TOTAL_TURNS){

            if (currentPlayer.behaviour.getTypeBehaviour() == "Cpu")
            {
                int waitTime = (int)Random.Range(1f, TURN_TIME / 2);
                StartCoroutine(waitToAct(waitTime));
            }

            yield return new WaitForSeconds(TURN_TIME);
            switchTurn();
        }
        else{
            OnTurnSystemFinished();

            Player winner = compareHeights();
            OnGameFinished(winner.id, winner.score);
        }
	}

    IEnumerator waitToAct(int howMuch)
    {
        yield return new WaitForSeconds(howMuch);
        Debug.Log("mandando actuación");
        currentPlayer.act();
    }

	void activatePlayer(Player player) {
		try { currentPlayer.deactivate(); } catch { }
		currentPlayer = player;
        OnPlayerTurn(currentTurn);
        OnTurnStarted();
        currentPlayer.activate();

        if (OnTimeStarts != null)
        {
            OnTimeStarts(TURN_TIME);
        }
        //Debug.Log("Turn " + currentTurn + ", Player " + currentPlayer.id + " at " + Time.time + ".");
    }

    //Lo puse publico para poder usarlo desde el onClick de los botones
	public void switchTurn() {

        //Esto evita que al pasar de TOTAL_TURNS, al accionar los botones siga intentano cambiar de turno
        if (currentTurn >= TOTAL_TURNS){
            OnTurnSystemFinished();

            Player winner = compareHeights();
            OnGameFinished(winner.id, winner.score);
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

    public void stopTurns()
    {
        StopCoroutine("turn");//Se para la corrutina para que no funcione unicamente en loop y se pueda cancelar al hundir un botón
    }

    private Player compareHeights()
    {
        Player Pwinner = null;

        if (players[0].score > players[1].score)
        {
            Pwinner = players[0];
        } else if (players[0].score < players[1].score)
        {
            Pwinner = players[1];
        } else
        {
            int r = Random.Range(0, 2);
            Pwinner = players[r];
        }

        return Pwinner;

    }
}
