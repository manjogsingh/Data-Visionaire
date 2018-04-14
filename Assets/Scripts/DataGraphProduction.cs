﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataGraphProduction : MonoBehaviour {

	private List<Dictionary<string, object>> pointList;
	//private bool bihar, punjab, haryana, mumbai, gujrat, bhopal, westBengal,uttarPradesh,hyderabad,jaipur = false;
	public string inputFile;
	public GameObject dataPointPrefab;
	public float plotScaleX = 100;
	public float plotScaleY = 100;

	public Text cropName;

	// Indices for columns to be assigned
	int year = 0;

	// Full column names
	public string xName;
	public string yName;

	void Awake () {
		pointList = CSVReader.Read (inputFile);
	}

	public void PlotCrop (int cropIndex, string lineName, float yMax = 370000f, float yMin = 1000f) {
		LineRenderer line = transform.Find (lineName).GetComponent<LineRenderer> ();
		line.positionCount = pointList.Count;
		List<string> columnList = new List<string> (pointList[1].Keys);
		// Assign column name from columnList to Name variables
		xName = columnList[year];
		yName = columnList[cropIndex];

		float xMax = 2015f;
		float xMin = 2005f;

		for (var i = 0; i < pointList.Count; i++) {
			// Get value in poinList at ith "row", in "column" Name, normalize
			float x = (System.Convert.ToSingle (pointList[i][xName]) - xMin) / (xMax - xMin);
			float y = (System.Convert.ToSingle (pointList[i][yName]) - yMin) / (yMax - yMin);

			// //instantiate the prefab with coordinates defined above

			// GameObject obj = Instantiate (dataPointPrefab); //, new Vector2 (x, y) , Quaternion.identity);
			// obj.transform.SetParent (transform);
			// obj.transform.localPosition = new Vector3 (x * 800, y * 500, 0);

			// // Assigns original values to dataPointName
			// string dataPointName =
			// 	lineName + " (" +
			// 	pointList[i][xName] + ", " +
			// 	pointList[i][yName] + ")";

			// // Assigns name to the prefab
			// obj.transform.name = dataPointName;
			// //obj.GetComponent<PointValue> ().setValue (dataPointName);
			// // Gets material color and sets it to a new RGB color we define
			// obj.GetComponent<Renderer> ().material.color = new Color (x, y, 1.0f);

			line.SetPosition (i, new Vector3 (x * plotScaleX, y * plotScaleY, 0));
		}
	}

	public void PlotFile () {
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild (i).gameObject.SetActive (true);
		}
		PlotCrop (1, "Rice");
		PlotCrop (2, "Maize");
		PlotCrop (3, "Wheat");
		PlotCrop (4, "Gram");
		PlotCrop (5, "Arhar");
		PlotCrop (6, "Moong");
		PlotCrop (7, "Ground Nuts");
		PlotCrop (8, "Mustard");
		PlotCrop (9, "Cotton");
		PlotCrop (10, "Sugercane");
	}

	public void DisableRest (string lineName) {
		for (int i = 0; i < transform.childCount; i++) {
			if (transform.GetChild (i).name.Equals (lineName)) {
				transform.GetChild (i).gameObject.SetActive (true);
				cropName.text = lineName;
				continue;
			}
			transform.GetChild (i).gameObject.SetActive (false);
		}
	}
}