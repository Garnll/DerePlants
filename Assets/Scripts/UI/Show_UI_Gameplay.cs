using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show_UI_Gameplay : MonoBehaviour {

    [SerializeField]
    private GameObject buttonContainer;
    private float positionForButtonContainer;

    public void Start()
    {
        Turn_Manager.OnTurnChanged += MoveButtons;

        if (buttonContainer == null)
        {
            Debug.LogError("No hay Contenedor de botones para show_ui_gameplay");
        }
        else
        {
            positionForButtonContainer = buttonContainer.transform.localPosition.x;
        }

    }

    public void OnDestroy()
    {
        Turn_Manager.OnTurnChanged -= MoveButtons;
    }

    public void MoveButtons (int currentTurn)
    {
        ShowButtons();

        if (currentTurn % 2 == 0)
        {
            //Jugador 2
            buttonContainer.transform.localPosition = new Vector3(-positionForButtonContainer, buttonContainer.transform.localPosition.y, buttonContainer.transform.localPosition.z);
        }
        else
        {
            //Jugador 1
            buttonContainer.transform.localPosition = new Vector3(positionForButtonContainer, buttonContainer.transform.localPosition.y, buttonContainer.transform.localPosition.z);
        }
    }

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
			//Debug.Log(_phraseSelector[i].chosenPhrase.myPhrase);
        }

	}

    public void HideButtons ()
    {
        buttonContainer.gameObject.SetActive(false);
    }

    public void ShowButtons()
    {
        buttonContainer.gameObject.SetActive(true);
    }
}
