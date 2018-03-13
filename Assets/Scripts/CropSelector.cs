using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CropSelector : MonoBehaviour {
	string cropName;
	// void Update () {
	// 	foreach (var toggle in gameObject.GetComponent<ToggleGroup> ().ActiveToggles ()) {
	// 		cropName = toggle.name;
	// 	}
	// }
	public void setCrop(string value){
		cropName=value;
	}

	public string SelectedCrop () {
		return cropName;
	}
}