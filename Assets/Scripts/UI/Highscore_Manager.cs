using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Highscore_Manager : MonoBehaviour {

    public float newHeight, oldHeight;
    public string newName, oldName;
    public Turn_Manager currentTurn;
    public Animator hsAnim;
    public int pos;


    [SerializeField]
    private Text name, playerWin, heightWin;


    private void Start()
    {
		hsAnim.SetBool("Finished", false);
        Turn_Manager.EventOnTurnSystemFinished += HighScoreAnim;
        Turn_Manager.EventOnGameFinished += ChangeNameHeight;
        
    }

    private void OnDestroy()
    {
        Turn_Manager.EventOnTurnSystemFinished -= HighScoreAnim;
        Turn_Manager.EventOnGameFinished -= ChangeNameHeight;
    }

    
    private void HighScoreAnim()
    {
            hsAnim.SetBool("Finished", true);
        
    }

    private void ChangeNameHeight(int ID, float score)
    {
        playerWin.text = "Player " + ID.ToString() + " won";
        heightWin.text = "Height: " + score.ToString("F") + " m";
        newHeight = score;


    }

    public void AddName()
    {
        newName = name.text.ToString();
        Debug.Log(newName);

    for (pos= 0;pos < 10;pos++)
        {
            Debug.Log(pos);
          /*  if (PlayerPrefs.HasKey(i + "HSCore"))
            {*/

                    oldHeight = PlayerPrefs.GetInt(pos + "HScore");
                    oldName = PlayerPrefs.GetString(pos + "HScoreName");
                    PlayerPrefs.SetInt(pos + "HScore", (int)newHeight);
                    PlayerPrefs.SetString(pos + "HScoreName", newName);
                newHeight = oldHeight;
                newName = oldName;         
            
           /* } else
            {
                PlayerPrefs.SetInt(i + "HScore", (int)newHeight);
                PlayerPrefs.SetString(i + "HScoreName", newName);
                newHeight = 10;
                newName = "lol";*/     


        }
        SceneManager.LoadScene("Menu");
    	}
    }

