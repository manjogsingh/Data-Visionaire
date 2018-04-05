using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ButtonController : MonoBehaviour {

	public Transform buttonParent;
	List<Transform> buttons;
	void Awake () {
		buttons = new List<Transform> ();
		buttonParent.GetComponentsInChildren<Transform> (buttons);
	}
	public void ButtonPress (string buttonName) {
		Transform button = null;
		foreach (Transform btn in buttons) {
			if (btn.name.Equals (buttonName)) {
				button = btn;
			}
		}Debug.Log(button.name+"  idhar");
		AnimateButton (button);
	}

	void AnimateButton (Transform button) {
		Sequence buttonPress = DOTween.Sequence ();
		buttonPress.Append (button.DOMoveZ (button.localPosition.z + -0.0070f, 1, false));//TODO y animation
		buttonPress.Append (button.DOMoveZ (button.localPosition.z, 1, false));//TODO return animation
		Debug.Log(button.name+"  udhar");
	}
}