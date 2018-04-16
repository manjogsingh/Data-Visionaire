using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CropSelector : MonoBehaviour {
	string cropName;
	public CropData cropData;
	public void setCrop (string value) {
		cropName = value;
		cropData.GetCropData (cropName.ToLower());
	}

	public string SelectedCrop () {
		return cropName;
	}
}