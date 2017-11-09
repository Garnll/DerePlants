using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Menu_Controller : MonoBehaviour {

    public string local, multiplayer, singleplayer, highscore;
    //public string[] sceneNames;
	
    public void PassScene()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;

        if (name == "Button.Local")
        {
            TypeOfParameter.Instance.currentPlayType = TypeOfParameter.Parameter.local;
            SceneManager.LoadScene(local);
        }

        if (name == "Button.Multiplayer")
        {
            TypeOfParameter.Instance.currentPlayType = TypeOfParameter.Parameter.network;
            SceneManager.LoadScene(multiplayer);
        }

        if (name == "Button.Singleplayer")
        {
            TypeOfParameter.Instance.currentPlayType = TypeOfParameter.Parameter.single;
            SceneManager.LoadScene(singleplayer);
        }

        if (name == "Button.Highscore")
            SceneManager.LoadScene(highscore);

    }
}
