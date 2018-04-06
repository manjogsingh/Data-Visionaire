using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ButtonController : MonoBehaviour {

	public Transform buttonParent;
	public GameObject subMenu;
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
		}
		AnimateButton (button);
	}

	public void ActivateSubMenu(Transform button){
		if(subMenu.activeSelf){
			subMenu.SetActive(false);
		}
		else{
			subMenu.SetActive(true);
			AnimateButton(button);
		}
	}

	void AnimateButton (Transform button) {
		DOTween.CompleteAll();
		button.DOPunchPosition(new Vector3(0,0,-0.0065f),1,1,1,false).IsComplete();
	}
}