using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Phrase {

    public int love;
    public Love_Type loveType;
    public string myPhrase;

    //Para el editor
    public bool editThisPhrase;

    //Para la pool
    public bool[] usedByPlayer = new bool[2];

    public Phrase(){
        for (int i = 0; i < usedByPlayer.Length; i++){
            usedByPlayer[i] = false;
        }
    }

	public void useBy(int player) {
		usedByPlayer[player] = true;
	}
}
