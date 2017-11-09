using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore_Manager : MonoBehaviour {

    public float newHeight, oldHeight;
    public string newName, oldName;
    public Turn_Manager currentTurn;

    [SerializeField]
    private Text name;

 

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
