using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Show_UI_Gameplay : NetworkBehaviour {

    [SerializeField]
    private GameObject buttonContainer;
    [SerializeField]
    private Text roundText;
    [SerializeField]
    private Image timerImage;
    [SerializeField]
    private Image stopperImage;

    private float positionForButtonContainer;
    private int currentRound = 0;

    private float maxTime;
    private float turnTime;

    private void Awake()
    {
        Turn_Manager.EventOnTimeStarts += UpdateTimer;
        Turn_Manager.EventOnTurnChanged += MoveButtons;
        Turn_Manager.EventOnPlayerTurn += UpdateRoundText;
        Turn_Manager.EventOnTurnSystemFinished += FinishUI;
    }

    private void Start()
    {
        //currentRound = 1;
        roundText.text = "Round " + currentRound;
        stopperImage.gameObject.SetActive(false);

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
        Turn_Manager.EventOnTurnChanged -= MoveButtons;
        Turn_Manager.EventOnPlayerTurn -= UpdateRoundText;
        Turn_Manager.EventOnTurnSystemFinished -= FinishUI;
        Turn_Manager.EventOnTimeStarts -= UpdateTimer;
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

        StopPlayerFromTouchingButtons(currentTurn);
    }

    private void StopPlayerFromTouchingButtons(int currentTurn)
    {
        switch (TypeOfParameter.Instance.currentPlayType)
        {
            case (TypeOfParameter.Parameter.local):

                break;

            case (TypeOfParameter.Parameter.network):
                break;

            case (TypeOfParameter.Parameter.single):
                if (currentTurn % 2 == 0)
                {
                    //Jugador 2
                    stopperImage.transform.position = buttonContainer.transform.position;
                    stopperImage.gameObject.SetActive(true);
                }
                else
                {
                    //Jugador 1
                    stopperImage.gameObject.SetActive(false);
                }
                break;

            default:
                break;
        }
    }

    private void UpdateRoundText(int currentTurn)
    {
        if (currentTurn % 2 != 0)
        {
            currentRound++;
            roundText.text = "Round " + currentRound;
        }
    }

    private void UpdateRoundText()
    {
        currentRound = 1;
        roundText.text = "Finished!";
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

    private void UpdateTimer(float maxTime)
    {
        CancelInvoke("UpdateTimer");
        this.maxTime = maxTime;

        turnTime = maxTime;
        timerImage.fillAmount = turnTime / this.maxTime;
        InvokeRepeating("UpdateTimer", Time.fixedDeltaTime, Time.fixedDeltaTime);
    }

    private void UpdateTimer()
    {
        turnTime -= Time.fixedDeltaTime;
        timerImage.fillAmount = turnTime / maxTime;

        if (turnTime < 0)
        {
            CancelInvoke("UpdateTimer");
        }
    }

    private void StopTimer()
    {
        CancelInvoke("UpdateTimer");
    }

    private void HideTimer()
    {
        StopTimer();
        timerImage.gameObject.SetActive(false);
    }

    public void HideButtons ()
    {
        StopTimer();
        buttonContainer.gameObject.SetActive(false);
    }

    public void ShowButtons()
    {
        buttonContainer.gameObject.SetActive(true);
    }

    private void FinishUI()
    {
        HideTimer();
        HideButtons();
        UpdateRoundText();
    }
}
