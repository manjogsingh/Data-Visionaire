using System.Collections;
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
	public Text xAxis, yAxis, xValue, yValue;

	// Indices for columns to be assigned
	int year = 0;

	// Full column names
	public string xName;
	public string yName;
	bool allCrop = false;

	void Awake () {
		pointList = CSVReader.Read (inputFile);
		for (int i = 0; i < transform.childCount; i++) {
			Transform t = transform.GetChild (i);
			for (int j = 0; j < 11; j++) {
				GameObject obj = Instantiate (dataPointPrefab);
				obj.SetActive (false);
				obj.transform.SetParent (t);
				obj.layer = 5;
				obj.tag = "DataPoint";
				obj.transform.localScale *= 2;
			}
		}
	}

	public void PlotCrop (int cropIndex, string lineName, float yMax = 370000f, float yMin = 1000f) {
		LineRenderer line = transform.Find (lineName).GetComponent<LineRenderer> ();
		line.positionCount = pointList.Count;
		List<string> columnList = new List<string> (pointList[1].Keys);
		// Assign column name from columnList to Name variables
		xName = columnList[year];
		yName = columnList[cropIndex];

		xAxis.text = xName;
		if (allCrop) {
			yAxis.text = "All Crop Productions";
		} else {
			yAxis.text = yName + " Production";
		}

		float xMax = 2015f;
		float xMin = 2005f;

		for (var i = 0; i < pointList.Count; i++) {
			// Get value in poinList at ith "row", in "column" Name, normalize
			float x = (System.Convert.ToSingle (pointList[i][xName]) - xMin) / (xMax - xMin);
			float y = (System.Convert.ToSingle (pointList[i][yName]) - yMin) / (yMax - yMin);

			// Assigns original values to dataPointName
			string dataPointName =
				"Crop " +
				lineName + " (" +
				pointList[i][xName] + ", " +
				pointList[i][yName] + ")";

			Transform parent = transform.Find (lineName);
			Transform child = parent.GetChild (i);

			child.gameObject.SetActive (true);
			child.localPosition = new Vector3 (x * plotScaleX, y * plotScaleY, 0);
			child.name = dataPointName;
			child.GetComponent<Renderer> ().material.color = line.startColor;

			line.SetPosition (i, new Vector3 (x * plotScaleX, y * plotScaleY, 0));
		}
	}

	public void PlotFile () {
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild (i).gameObject.SetActive (true);
		}
		allCrop = true;
		PlotCrop (1, "Rice");
		PlotCrop (2, "Maize");
		PlotCrop (3, "Wheat");
		PlotCrop (4, "Gram");
		PlotCrop (5, "Arhar");
		PlotCrop (6, "Moong");
		PlotCrop (7, "Ground Nuts");
		PlotCrop (8, "Mustard");
		PlotCrop (9, "Cotton");
		PlotCrop (10, "Sugarcane");
		allCrop = false;
	}

	public void DisableRest (string lineName) {
		for (int i = 0; i < transform.childCount; i++) {
			if (transform.GetChild (i).name.Equals (lineName)) {
				transform.GetChild (i).gameObject.SetActive (true);
				continue;
			}
			transform.GetChild (i).gameObject.SetActive (false);
		}
	}

	public void SetValue (Transform hit) {
		string name = hit.name;
		if (name.StartsWith ("Crop")) {
			int length = name.IndexOf (")") - name.IndexOf (",") - 2;
			xValue.text = "X = " + name.Substring (name.IndexOf ("(") + 1, 4);
			yValue.text = "Y = " + name.Substring (name.IndexOf (",") + 2, length);
		}
	}
}