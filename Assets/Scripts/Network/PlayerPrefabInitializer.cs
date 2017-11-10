using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerPrefabInitializer : NetworkBehaviour {

    PlayersController playersController;

    PlayerGameObject myPlayer;

    public override void OnStartLocalPlayer()
    {
        if (TypeOfParameter.Instance.currentPlayType != TypeOfParameter.Parameter.network)
        {
            return;
        }

        playersController = FindObjectOfType<PlayersController>();

        myPlayer = playersController.player1;
        if (myPlayer.player.controlled == false)
        {
            myPlayer.player.controlled = true;
        }
        else
        {
            myPlayer = playersController.player2;
            if (myPlayer.player.controlled == false)
            {
                myPlayer.player.controlled = true;
            }
            else
            {
                Debug.LogError("Hello Darkness my old friend");
            }
        }
    }
}
