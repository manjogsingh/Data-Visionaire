using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataParser : MonoBehaviour {

	public string url = "https://dae71ed4.ngrok.io/annualrainfall";
	//public string url = "https://api.myjson.com/bins/187sb1";
	//public string url ="https://api.myjson.com/bins/k3fkx";
	WWW www;
	JsonData jsonObject;
	IEnumerator Start () {
		www = new WWW (url);
		yield return www;
		jsonObject = JsonMapper.ToObject (www.text);
		//Debug.Log (jsonObject[0]["name"]);
		//Debug.Log(jsonObject[0]["annual_rainfall"][0]["2000"]);
		//Debug.Log(jsonObject[0]["annual_rainfall"][0]["New Delhi"]); aditya
	}
}