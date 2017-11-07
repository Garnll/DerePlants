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
		player2.player = new Player(2, new CpuBehaviour());
	}

}
