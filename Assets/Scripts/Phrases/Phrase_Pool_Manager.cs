﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phrase_Pool_Manager : MonoBehaviour
{
    //Singleton
    private static Phrase_Pool_Manager phrasePoolManager;

    public static Phrase_Pool_Manager Instance()
    {
        if (!phrasePoolManager)
        {
            phrasePoolManager = FindObjectOfType(typeof(Phrase_Pool_Manager)) as Phrase_Pool_Manager;
            if (!phrasePoolManager)
                Debug.LogError("There needs to be at least one GameObject with an Phrase_Pool_Manager script attached to it!");
        }

        return phrasePoolManager;
    }



    private Phrase_Pool phrasesPool;

    private int currentTurnOnPool;

    private List<Phrase> phrasesOnPool = new List<Phrase>();
    private List<Phrase> phrasesUsed = new List<Phrase>();

    private void Start()
    {
        phrasesPool = GameObject.FindObjectOfType<Phrase_Pool>();
        if (phrasesPool == null)
        {
            Debug.LogError("Al menos un GameObject debe tener el script Phase_Pool");
        }

        AddPoolsToUltraPool(phrasesPool.phrasesPositive);
        AddPoolsToUltraPool(phrasesPool.phrasesNegative);
        AddPoolsToUltraPool(phrasesPool.phrasesAmbiguous);
    }

    private void AddPoolsToUltraPool(Phrase[] pool)
    {
        for (int i = 0; i < pool.Length; i++)
        {
            phrasesOnPool.Add(pool[i]);
        }
    }

    public int PhrasesOnPoolCount
    {
        get
        {
            return phrasesOnPool.Count;
        }
    }


    public Phrase PhrasePickOne(int index, Love_Type tipo)
    {
        Phrase phraseToSend;

        if (index < phrasesOnPool.Count)
        {
            if (phrasesOnPool[index].usedByPlayer[currentTurnOnPool - 1] == false)
            {
                if (phrasesOnPool[index].loveType == tipo)
                {
                    phraseToSend = phrasesOnPool[index];
                }
                else
                {
                    phraseToSend = null;
                }
            }
            else
            {
                phraseToSend = null;
            }
        }
        else
        {
            phraseToSend = null;
            Debug.LogError("Enviando index mayor a la lista de Phrases");
        }

        return phraseToSend;
    }

    public void PhraseFreeOne(int index)
    {
        phrasesOnPool[index].usedByPlayer[currentTurnOnPool - 1] = true;

        bool allPlayersHaveUsedIt = false;

        foreach (bool used in phrasesOnPool[index].usedByPlayer)
        {
            if (used == true)
            {
                allPlayersHaveUsedIt = true;
            }
            else
            {
                allPlayersHaveUsedIt = false;
                break;
            }
        }

        if (allPlayersHaveUsedIt == true)
        {
            phrasesUsed.Add(phrasesOnPool[index]);
        }
    }

    //Recibirá un evento de cambio de turno
    private void ChangeTurnOnPool(int newTurn)
    {
        currentTurnOnPool = newTurn;
    }
}
