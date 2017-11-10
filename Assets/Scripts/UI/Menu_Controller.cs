using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menu_Controller : MonoBehaviour {

	public string local, host, join, singleplayer;
	public Animator highscoreAnimator;

	public Text[] listOfRanks;


	public void HighscoreAnim() {

		setRanks ();

		highscoreAnimator.SetBool("HighscoreUp", !highscoreAnimator.GetBool("HighscoreUp")); 
		highscoreAnimator.SetTrigger ("TriggerAnim");
	}

	public void setRanks() {
		for (int i = 0; i < listOfRanks.Length; i++) {
			listOfRanks[i].text = string.Format("{0}: {1}",PlayerPrefs.GetString(i + "HScoreName"), PlayerPrefs.GetInt(i + "HScore"));
		}
	
	
	}
    //public string[] sceneNames;
	
    public void PassScene()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;

        if (name == "Button.Local")
        {
            TypeOfParameter.Instance.currentPlayType = TypeOfParameter.Parameter.local;
            SceneManager.LoadScene(local);
        }

        if (name == "Button.Join")
        {
            TypeOfParameter.Instance.currentPlayType = TypeOfParameter.Parameter.network;
			SceneManager.LoadScene(join);
        }

		if (name == "Button.Host")
		{
			TypeOfParameter.Instance.currentPlayType = TypeOfParameter.Parameter.network;
			SceneManager.LoadScene(host);
		}

        if (name == "Button.Singleplayer")
        {
            TypeOfParameter.Instance.currentPlayType = TypeOfParameter.Parameter.single;
            SceneManager.LoadScene(singleplayer);
        }

		if (name == "Button.Highscore")
			HighscoreAnim ();

    }
}
