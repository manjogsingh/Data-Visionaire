using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataPlotter : MonoBehaviour {

	private List<Dictionary<string, object>> pointList;
	private bool bihar, punjab, haryana, mumbai, gujrat, bhopal, westBengal, uttarPradesh, hyderabad, jaipur = false;
	public string inputFile;
	public GameObject dataPointPrefab;
	public Transform plottedPoints;
	public float plotScale = 8;
	public Transform biharTransform, punjabTransform, mumbaiTransform, haryanaTransform, gujratTransform, bhopalTransform, westBengalTransform, uttarPradeshTransform, hyderabadTransform, jaipurTransform;

	// Indices for columns to be assigned
	int columnX = 0;
	int columnY = 1;
	int columnZ = 2;
	int columnW = 3;

	// Full column names
	string xName;
	string yName;
	string zName;
	string wName;

	public Text xAxis, yAxis, zAxis;

	void PlotFile (string fileName, Transform parent) {
		inputFile = fileName;
		pointList = CSVReader.Read (inputFile);
		List<string> columnList = new List<string> (pointList[1].Keys);

		// Assign column name from columnList to Name variables
		xName = columnList[columnX];
		yName = columnList[columnY];
		zName = columnList[columnZ];
		wName = columnList[columnW];

		xAxis.text = wName;
		yAxis.text = yName;
		zAxis.text = zName;

		//Only for single csv
		// // Get maxes of each axis
		// float xMax = FindMaxValue (xName);
		// float yMax = FindMaxValue (yName);
		// float zMax = FindMaxValue (zName);
		// float wMax = FindMaxValue (wName);

		// // Get minimums of each axis
		// float xMin = FindMinValue (xName);
		// float yMin = FindMinValue (yName);
		// float zMin = FindMinValue (zName);
		// float wMin = FindMinValue (wName);

		float xMax = 2015f;
		float yMax = 30f;
		float zMax = 80f;
		float wMax = 1800f;

		// // Get minimums of each axis
		float xMin = 2005;
		float yMin = 20f;
		float zMin = 30f;
		float wMin = 300f;

		//Loop through Pointlist
		for (var i = 0; i < pointList.Count; i++) {
			// Get value in poinList at ith "row", in "column" Name, normalize
			float x = (System.Convert.ToSingle (pointList[i][xName]) - xMin) / (xMax - xMin);
			float y = (System.Convert.ToSingle (pointList[i][yName]) - yMin) / (yMax - yMin);
			float z = (System.Convert.ToSingle (pointList[i][zName]) - zMin) / (zMax - zMin);
			float w = (System.Convert.ToSingle (pointList[i][wName]) - wMin) / (wMax - wMin);
			//instantiate the prefab with coordinates defined above

			GameObject obj = Instantiate (dataPointPrefab, new Vector3 (y, z, w) * plotScale, Quaternion.identity);
			obj.transform.SetParent (parent);

			// Assigns original values to dataPointName
			string dataPointName =
				inputFile + " " +
				pointList[i][xName] + " (" +
				pointList[i][yName] + ", " +
				pointList[i][zName] + ", " +
				pointList[i][wName] + ")";

			// Assigns name to the prefab
			obj.transform.name = dataPointName;
			obj.GetComponent<PointValue> ().setValue (dataPointName);
			// Gets material color and sets it to a new RGB color we define
			obj.GetComponent<Renderer> ().material.color = new Color (x, y, z, 1.0f);
		}
	}

	float FindMaxValue (string columnName) {
		//set initial value to first value
		float maxValue = System.Convert.ToSingle (pointList[0][columnName]);

		//Loop through Dictionary, overwrite existing maxValue if new value is larger
		for (var i = 0; i < pointList.Count; i++) {
			if (maxValue < System.Convert.ToSingle (pointList[i][columnName]))
				maxValue = System.Convert.ToSingle (pointList[i][columnName]);
		}
		return maxValue;
	}

	float FindMinValue (string columnName) {

		float minValue = System.Convert.ToSingle (pointList[0][columnName]);

		//Loop through Dictionary, overwrite existing minValue if new value is smaller
		for (var i = 0; i < pointList.Count; i++) {
			if (System.Convert.ToSingle (pointList[i][columnName]) < minValue)
				minValue = System.Convert.ToSingle (pointList[i][columnName]);
		}
		return minValue;
	}

	public void selectState (string stateName) {
		switch (stateName) {
			case "Bihar":
				ToogleState (ref bihar, stateName, biharTransform);
				//	break;
				//case "Punjab":
				ToogleState (ref punjab, "Punjab", punjabTransform);
				//	break;
				//case "Mumbai":
				ToogleState (ref mumbai, "Mumbai", mumbaiTransform);
				//	break;
				//case "Haryana":
				ToogleState (ref haryana, "Haryana", haryanaTransform);
				//	break;
				//case "Gujrat":
				ToogleState (ref gujrat, "Gujrat", gujratTransform);
				//	break;
				//case "Bhopal":
				ToogleState (ref bhopal, "Bhopal", bhopalTransform);
				//break;
				//case "Bhopal":
				ToogleState (ref westBengal, "West Bengal", westBengalTransform);
				//break;
				//case "Bhopal":
				ToogleState (ref uttarPradesh, "Uttar Pradesh", uttarPradeshTransform);
				//break;
				//case "Bhopal":
				ToogleState (ref hyderabad, "Hyderabad", hyderabadTransform);
				//break;
				//case "Bhopal":
				ToogleState (ref jaipur, "Jaipur", jaipurTransform);
				break;
		}
	}

	void ToogleState (ref bool state, string stateName, Transform stateTransform) {
		state = !state;
		if (state) {
			PlotFile (stateName, stateTransform);
		} else {
			for (int i = 0; i < stateTransform.childCount; i++) {
				Destroy (stateTransform.GetChild (i).gameObject);
			}
		}
	}
}