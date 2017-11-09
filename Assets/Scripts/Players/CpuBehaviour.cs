using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CpuBehaviour : IBehaviour {

    private Input_Manager inputManager;
    private WeatherManager wheaterManager;

	public string type = "Cpu";

	public void act() {
        if (inputManager == null)
        {
            inputManager = Input_Manager.Instance();
        }
        if (wheaterManager == null)
        {
            wheaterManager = GameObject.FindObjectOfType<WeatherManager>();
        }

        if (wheaterManager.currentWeather != wheaterManager.weathers[0])
        {
            giveLove();
        }
        else
        {
            input(); //Hará la selección del objeto según el clima
        }

    }
	public void giveLove() {
        int buttonToPress = Random.Range(0, inputManager.ButtonNumber);
        Debug.Log("cpu actuando");

        //inputManager.StopCoroutine(inputManager.PressButton(buttonToPress));

        inputManager.StartCoroutine(inputManager.PressButton(buttonToPress));
	}

	public void input() {
        Debug.Log("cpu actuando en input");
        giveLove(); //momentarily
    }

	public void useItem(Item i) {

	}

	public string getTypeBehaviour() {
		return type;
	}
}
