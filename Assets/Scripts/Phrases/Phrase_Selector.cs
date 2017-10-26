using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phrase_Selector : MonoBehaviour {

    private Phrase_Pool_Manager phrasePoolManager;

    [HideInInspector]
    public Phrase chosenPhrase = null;
    [HideInInspector]
    public Love_Type phraseType;

    public Text myText;

	// Use this for initialization
	private void Start ()
    {
        phrasePoolManager = Phrase_Pool_Manager.Instance();
        myText = GetComponentInChildren<Text>();
	}
	
    public void ChoosePhrase()
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
    }

    public void LoosePhrase()
    {
        if (chosenPhrase != null)
        {
            chosenPhrase = null;
        }
    }
}
