using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Manager : MonoBehaviour {

    //Singleton
    private static Input_Manager inputManager;

    public static Input_Manager Instance()
    {
        if (!inputManager)
        {
            inputManager = FindObjectOfType(typeof(Input_Manager)) as Input_Manager;
            if (!inputManager)
                Debug.LogError("There needs to be at least one GameObject with an Phrase_Pool_Manager script attached to it!");
        }

        return inputManager;
    }



}
