using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show_UI_Gameplay : MonoBehaviour {

	public static void UpdatePhrases (Phrase_Selector[] _phraseSelector)
    {
		if (_phraseSelector == null)
        {
            _phraseSelector = FindObjectsOfType<Phrase_Selector>();
            if (_phraseSelector.Length > 3)
            {
                Debug.LogError("Muchos Phrase_Selector en la escena");
            }
            else if (_phraseSelector.Length < 3)
            {
                Debug.LogError("No hay suficientes Phrase_Selector en la escena");
            }
        }

        for (int i = 0; i < _phraseSelector.Length; i++)
        {
            _phraseSelector[i].myText.text = _phraseSelector[i].chosenPhrase.myPhrase;
			Debug.Log(_phraseSelector[i].chosenPhrase.myPhrase);
        }

	}
}
