using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Menu_Controller : MonoBehaviour {

    public string local, multiplayer, singleplayer, highscore;
    //public string[] sceneNames;

	// Use this for initialization
	void Start () {
		
	}
	
    public void PassScene()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;

        if (name == "Button.Local")
            SceneManager.LoadScene(local);
        if (name == "Button.Multiplayer")
            SceneManager.LoadScene(multiplayer);
        if (name == "Button.Singleplayer")
            SceneManager.LoadScene(singleplayer);
        if (name == "Button.Highscore")
            SceneManager.LoadScene(highscore);

    }

	// Update is called once per frame
	void Update () {
		
	}
}
