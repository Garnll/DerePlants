using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phrase_Selector : MonoBehaviour {

    private Phrase_Pool_Manager phrasePoolManager;
    public Phrase chosenPhrase = null;
    public Love_Type phraseType;

	// Use this for initialization
	private void Start ()
    {
        phrasePoolManager = Phrase_Pool_Manager.Instance();	
	}
	
    public Phrase ChoosePhrase()
    {
        if (phrasePoolManager.PhrasesOnPoolCount > 0)
        {
            int randomNumber = 0;
            while (chosenPhrase == null)
            {
                randomNumber = Random.Range(0, phrasePoolManager.PhrasesOnPoolCount - 1);
                chosenPhrase = phrasePoolManager.PhrasePickOne(randomNumber, phraseType);
            }

            phrasePoolManager.PhraseFreeOne(randomNumber);
        }
        else
        {
            Debug.Log("Se está intentando conseguir más frases cuando ya no hay");
        }

        return chosenPhrase;
    }

    public void LoosePhrase()
    {
        chosenPhrase = null;
    }
}
