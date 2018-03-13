using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointValue : MonoBehaviour {

	public TextMesh pointValue;
	// Use this for initialization
	void Awake () {
		pointValue = GetComponentInChildren<TextMesh> ();
		pointValue.text="";
	}

	public void setValue (string value) {
		pointValue.text = value;
	}
}