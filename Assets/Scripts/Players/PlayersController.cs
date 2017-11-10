using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersController : MonoBehaviour {

	[SerializeField]
	public PlayerGameObject player1;

	[SerializeField]
	public PlayerGameObject player2;

	private void Start() {

        Turn_Manager.OnTurnSystemFinished += getFinalHeight;

        switch (TypeOfParameter.Instance.currentPlayType)
        {
            case (TypeOfParameter.Parameter.local):
                player1.player = new Player(1, new HumanBehaviour());
                player2.player = new Player(2, new HumanBehaviour());
                break;

            case (TypeOfParameter.Parameter.network):

                player1.player = new Player(1, new HumanBehaviour());
                player2.player = new Player(2, new HumanBehaviour()); //Aqui no se qué poner ayuda
                break;

            case (TypeOfParameter.Parameter.single):
                player1.player = new Player(1, new HumanBehaviour());
                player2.player = new Player(2, new CpuBehaviour());
                break;

            default:
                player1.player = new Player(1, new HumanBehaviour());
                player2.player = new Player(2, new CpuBehaviour());
                break;
        }
	}

    public void getFinalHeight()
    {
        player1.player.score = (int)player1.plant.height;
        player2.player.score = (int)player2.plant.height;
    }
}
