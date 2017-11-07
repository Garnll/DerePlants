using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Graphical_Decorator : Graphical_Component {

	[SerializeField]
	public Sprite sprite;

	SpriteRenderer rend;

	// Use this for initialization
	void Start() {
		rend = GetComponent<SpriteRenderer>();
		rend.sprite = sprite;
	}

	// Update is called once per frame
	void Update() {

	}
}
