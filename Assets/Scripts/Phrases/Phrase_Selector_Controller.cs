using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phrase_Selector_Controller : MonoBehaviour {

    private Phrase_Selector[] phraseSelector;

    private void Start()
    {
        phraseSelector = FindObjectsOfType<Phrase_Selector>();
        if (phraseSelector.Length > 3)
        {
            Debug.LogError("Muchos Phrase_Selector en la escena");
        }
    }

    public void SendLoveType()
    {
        Love_Type[] loveType = new Love_Type[3];
        loveType[0] = Love_Type.kind;
        loveType[1] = Love_Type.hate;
        loveType[2] = Love_Type.ambigous;

    }

}
