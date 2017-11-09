using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant_Creator : MonoBehaviour{

	[SerializeField]
	Sprite[] heads;

	[SerializeField]
	Sprite[] stems;

	enum PlantPersonality { Tsundere, Normie }
	string[] personalityNames;

	
	void Start() {
		fillPersonalities();
	}

	public Seed newRandomPlant() {
		Sprite randomHead = heads[randomIndexOf(heads)];
		Sprite randomStem = stems[randomIndexOf(stems)];
		string randomPersonality = personalityNames[randomIndexOf(personalityNames)];
		Debug.Log(randomHead.name + " + " + randomStem.name + " + " + randomPersonality);
		return new Seed(randomPersonality, randomHead, randomStem);
	}

	void fillPersonalities() {
		personalityNames = System.Enum.GetNames(typeof(PlantPersonality));
	}

	int randomIndexOf(Sprite[] array) {
		int rand = Random.Range(0, array.Length);
		return rand;
	}

	int randomIndexOf(string[] array) {
		int rand = Random.Range(0, array.Length);
		return rand;
	}




}
