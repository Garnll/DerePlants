using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather {

	public int id;
	public string name;
	public int magnitude;
	public int turnsBeing;

	public Weather(int i, int m, string n) {
		id = i;
		magnitude = m;
		name = n;
	}

	public void reset() {
		turnsBeing = 0;
	}

	public int getDealingDamage() {
		return (5 * (magnitude + turnsBeing) / 3); 
	}


}
