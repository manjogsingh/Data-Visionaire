using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class CropData : MonoBehaviour {

	WWW www;
	JsonData jsonObject;
	public string url = "https://e5c1f58e.ngrok.io/prediction?crop=";
	public int cropCount=0;
	public List<string> cropValues = new List<string> ();
	Dictionary<string, int> stateName = new Dictionary<string, int> ();

	void Start () {
		stateName.Add ("Bhopal", 1);
		stateName.Add ("Bihar", 1);
		stateName.Add ("Gujarat", 1);
		stateName.Add ("Haryana", 1);
		stateName.Add ("Hyderabad", 1);
		stateName.Add ("Jaipur", 1);
		stateName.Add ("Mumbai", 1);
		stateName.Add ("Punjab", 1);
		stateName.Add ("Uttar Pradesh", 1);
		stateName.Add ("West Bengal", 1);
	}

	public void GetCropData (string cropName) {
		StartCoroutine (YieldData (url + cropName));
	}

	IEnumerator YieldData (string uri) {
		www = new WWW (uri);
		yield return www;
		while (true) {
			if (www.isDone) {
				jsonObject = JsonMapper.ToObject (www.text);

				cropValues.Clear ();
				cropCount=0;
				foreach (string key in stateName.Keys) {
					int a = (int) jsonObject[key];
					if (a == 0) {
						cropValues.Add (key);
						cropCount++;
					}
				}
				break;
			} else {
				new WaitForSeconds (1.0f);
			}
		}
	}
}