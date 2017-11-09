using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant_Controller : MonoBehaviour {

    /// <summary>
    /// All of this is temp
    /// </summary>

    private int growmultiplicator;

    //private Plant[] plants;

    [SerializeField]
    private PlayersController plController;

    private void Awake()
    {
        if (plController == null)
        {
            plController = FindObjectOfType<PlayersController>();
        }
    }

    private void ReceiveHeight(Plant plantToGrow,int growThisMuch)
    {
        plantToGrow.Grow(growThisMuch);
    }

    public void ReceiveLove(Phrase phrase)
    {
        Plant plantToUse = null;

        if (plController.player1.player.onTurn)
        {
            plantToUse = plController.player1.plant;
        }
        else if (plController.player2.player.onTurn)
        {
            plantToUse = plController.player2.plant;
        }
        else
        {
            Debug.LogError("Somehow neither player is onTurn");
            return;
        }

        growmultiplicator = plantToUse.CheckLove(phrase.loveType);

        int newHeight = phrase.love * growmultiplicator;

        ReceiveHeight(plantToUse, newHeight);
    }

    private void ReceivePowerUp()
    {

    }

    private void ReceiveClimateAvertion(int ungrow)
    {

    }
}
