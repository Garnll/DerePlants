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

	public Graphical_Plant newRandomPlant() {
		Graphical_Plant seed = new Graphical_Plant();
		seed.sow();
		seed.head.sprite = heads[randomIndexOf(heads)];
		seed.stem.sprite = stems[randomIndexOf(stems)];
		seed.personality.name = personalityNames[randomIndexOf(personalityNames)];
		return seed;
	}

	void fillPersonalities() {
		personalityNames = System.Enum.GetNames(typeof(PlantPersonality));
	}

	int randomIndexOf(Sprite[] array) {
		int rand = Random.Range(0, array.Length - 1);
		return rand;
	}

	int randomIndexOf(string[] array) {
		int rand = Random.Range(0, array.Length - 1);
		return rand;
	}


}
