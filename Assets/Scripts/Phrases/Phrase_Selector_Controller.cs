using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        else if (phraseSelector.Length < 3)
        {
            Debug.LogError("No hay suficientes Phrase_Selector en la escena");
        }

        PickNewPhrases(); //De momento
    }

    public void PickNewPhrases()
    {
        SendLoveType();

        for (int i = 0; i < phraseSelector.Length; i++)
        {
            phraseSelector[i].LoosePhrase();

            phraseSelector[i].ChoosePhrase();
        }

        Show_UI_Gameplay.UpdatePhrases(phraseSelector);
    }

    private void SendLoveType()
    {
        Love_Type[] loveType = new Love_Type[3];

        loveType = RandomizeTypeArray(loveType);

        for (int i = 0; i < phraseSelector.Length; i++)
        {
            phraseSelector[i].phraseType = loveType[i];
        }
    }

    private Love_Type[] RandomizeTypeArray(Love_Type[] original)
    {
        System.Random rnd = new System.Random();

        var numbers = Enumerable.Range(Enum.GetValues(typeof(Love_Type)).Cast<int>().Min(), original.Length)
            .OrderBy(r => rnd.Next()).ToArray();

        for (int i = 0; i < original.Length; i++)
        {
            original[i] = (Love_Type)numbers[i];
        }

        return original;
    }
}
