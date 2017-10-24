using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phrase_Pool : MonoBehaviour {

    [SerializeField]
    public Phrase[] phrasesPositive = new Phrase[10];
    [SerializeField]
    public Phrase[] phrasesAmbiguous = new Phrase[10];
    [SerializeField]
    public Phrase[] phrasesNegative = new Phrase[10];

}
