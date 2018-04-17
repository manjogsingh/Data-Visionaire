using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class DataParser : MonoBehaviour {

	//public string url = "https://dae71ed4.ngrok.io/annualrainfall";
	public string url = "https://api.myjson.com/bins/12ixox";
	//public string url = "https://api.myjson.com/bins/187sb1";
	//public string url ="https://api.myjson.com/bins/k3fkx";
	WWW www;
	JsonData jsonObject;
	States[] states;
	IEnumerator Start () {
		www = new WWW (url);
		yield return www;
		jsonObject = JsonMapper.ToObject (www.text);
		// Debug.Log (jsonObject[0]["name"]);
		// Debug.Log (jsonObject[0]["annual_rainfall"][0]["2000"]);
		//Debug.Log(jsonObject[0]["annual_rainfall"][0]["New Delhi"]); aditya

		string value = www.text;
		value = fixJson (value);
		states = JsonHelper.FromJson<States> (value);
		for (int i = 0; i < jsonObject.Count; i++) {
			string v = JsonMapper.ToJson (jsonObject[i]["annual_rainfall"]);
			v = fixJson (v);
			states[i].annualRainfall = JsonHelper.FromJson<AnnualRainfall> (v);
			//Debug.Log (states[i].annualRainfall[i].year[(2000+i).ToString()]);
		}
	}
	string fixJson (string value) {
		value = "{\"Items\":" + value + "}";
		return value;
	}

	public string GetRainfallValues(string name,string year){
		int y=int.Parse(year) %2000;
		// string ys="y"+year;
		for(int i=0;i<jsonObject.Count;i++)
		{
			Debug.Log(states[i].name);
			Debug.Log(year);
			Debug.Log(states[i].annualRainfall[y].year.Keys.ToString());
			if(states[i].name.Equals(name)){
				return states[i].annualRainfall[y].year[year];
				//return ys;//states[i].annualRainfall[y]
			}
		}
		return "lol";
	}

	//Old 

	// public string GetRainfallValues (string name, string year) {
	// 	for (int i = 0; i < jsonObject.Count; i++) {
	// 		if (jsonObject[i]["name"].Equals (name)) {
	// 			for (int j = 0; j < 16; j++) {
	// 				if ((j + 2000).ToString ().Equals (year)) {
	// 					Debug.Log (year);
	// 					return jsonObject[i]["annual_rainfall"][j]["y"+(j + 2000).ToString ()].ToString ();
	// 				}
	// 			}
	// 		}
	// 	}
	// 	return "lol";
	// }
}