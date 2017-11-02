using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Input_Manager : MonoBehaviour {

    //Singleton
    private static Input_Manager inputManager;

    public static Input_Manager Instance()
    {
        if (!inputManager)
        {
            inputManager = FindObjectOfType(typeof(Input_Manager)) as Input_Manager;
            if (!inputManager)
                Debug.LogError("There needs to be at least one GameObject with an Input_Manager script attached to it!");
        }

        return inputManager;
    }

    [SerializeField]
    private Button[] phrasesButtons;

    //Cosasa temporales. Eliminar cuando se haga bien
    [SerializeField]
    private Turn_Manager turnManager;
    [SerializeField]
    private Plant_Controller plantController;

    private void Start()
    {
        if (phrasesButtons == null || phrasesButtons.Length == 0)
        {
            Debug.Log("Should add Buttons to Input Manager");

            Phrase_Selector[] tempPhraseSelector = FindObjectsOfType<Phrase_Selector>();

            phrasesButtons = new Button[tempPhraseSelector.Length];

            for (int i = 0; i < tempPhraseSelector.Length; i++)
            {
                phrasesButtons[i] = tempPhraseSelector[i].gameObject.GetComponent<Button>();
            }
        }
        if (turnManager == null)
        {
            turnManager = FindObjectOfType<Turn_Manager>(); //temp
        }
        if (plantController == null)
        {
            plantController = FindObjectOfType<Plant_Controller>(); //temp
        }

        for (int i = 0; i < phrasesButtons.Length; i++)
        {
            int temp = i;
            phrasesButtons[i].onClick.AddListener(() => ButtonPressed(phrasesButtons[temp].GetComponent<Phrase_Selector>()));
        }
    }

    public void ButtonPressed(Phrase_Selector phraseSelectorInButton)
    {
        plantController.ReceiveLove(phraseSelectorInButton.chosenPhrase); //Más temporal que tu p

        turnManager.switchTurn(); //Temporal (se finalizará el turno al hacer animaciones
    }
}
