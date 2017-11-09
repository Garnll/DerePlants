using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore_Manager : MonoBehaviour {

    public float newHeight, oldHeight;
    public string newName, oldName;
    public Turn_Manager currentTurn;
    public Animator hsAnim;

    [SerializeField]
    private Text name, playerWin, heightWin;


    private void Start()
    {
        Turn_Manager.OnTurnSystemFinished += HighScoreAnim;
        Turn_Manager.OnGameFinished += ChangeNameHeight;


    }

    private void HighScoreAnim()
    {
        //hsAnim.Play("HighscoreFinishDrop");
        hsAnim.SetBool("Finished", true);
    }

    private void ChangeNameHeight(int ID, float score)
    {
        playerWin.text = "Player " + ID.ToString() + " won";
        heightWin.text = "Height: " + score.ToString("F");

    }

    public void AddName()
    {
        newName = name.text.ToString();
        Debug.Log(newName);

    for (int i= 0;i< 10;i++)
        {
            if (PlayerPrefs.HasKey(i + "HSCore"))
            {
            
                    //oldHeight = PlayerPrefs.GetInt(i + "HScore");
                    oldName = PlayerPrefs.GetString(i + "HScoreName");
                   // PlayerPrefs.SetInt(i + "HScore", newHeight);
                    PlayerPrefs.SetString(i + "HScoreName", newName);


                             
            }

        }


    }

}
