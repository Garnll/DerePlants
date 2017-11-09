using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersController : MonoBehaviour {

	[SerializeField]
	public PlayerGameObject player1;

	[SerializeField]
	public PlayerGameObject player2;

	private void Start() {
		player1.player = new Player(1, new HumanBehaviour());

        switch (TypeOfParameter.Instance.currentPlayType)
        {
            case (TypeOfParameter.Parameter.local):
                player2.player = new Player(2, new HumanBehaviour());
                break;

            case (TypeOfParameter.Parameter.network):
                //player2.player = new Player(2, new HumanBehaviour()); //Aqui no se qué poner ayuda
                break;

            case (TypeOfParameter.Parameter.single):
                player2.player = new Player(2, new CpuBehaviour());
                break;
        }
	}

}
