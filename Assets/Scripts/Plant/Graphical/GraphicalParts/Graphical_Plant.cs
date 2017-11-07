using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graphical_Plant : Graphical_Component {

	public Graphical_Head head;
	public Graphical_Stem stem;
	public Personality personality;

	public void sow() {
		head = new Graphical_Head();
		stem = new Graphical_Stem();
		personality = new Personality();
	}

}
