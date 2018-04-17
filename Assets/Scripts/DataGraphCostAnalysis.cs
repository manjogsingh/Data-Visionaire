using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GraphData {
	public float dataPoint;
}

public class DataGraphCostAnalysis : MonoBehaviour {

	private List<Dictionary<string, object>> pointList;
	public string inputFile;
	public GameObject dataPointPrefab;
	public float plotScaleY;

	public string xName;
	public string yName;
	public string zName;

	public Text xAxis, yAxis, zAxis;

	float xMax, yMax, xMin, yMin;

	// int costOfCultivation = 2;
	// int costOfCultivationD = 3;
	// int costOfProduction = 4;
	int crop = 0;
	int state = 1;
	int yield = 5;
	int costOfCultivationPerHectare = 6;
	int perHectareCostPrice = 7;
	int yieldKG = 8;

	void Awake () {
		pointList = CSVReader.Read (inputFile);
	}

	void PlotGraph (int xIndex, int yIndex) {
		Clear ();
		List<string> columnList = new List<string> (pointList[1].Keys);

		// Assign column name from columnList to Name variables
		xName = columnList[xIndex];
		yName = columnList[yIndex];
		if (xIndex == 1) {
			zName = columnList[crop];
			xAxis.text = xName;
			yAxis.text = yName;
			zAxis.text = zName;
		} else {
			zName = columnList[state];
			xAxis.text = zName;
			yAxis.text = yName;
			zAxis.text = xName;
		}

		float xMax = 4;
		float yMax = FindMaxValue (yName);

		float xMin = 0;
		float yMin = FindMinValue (yName);

		//Loop through Pointlist
		for (var i = 0; i < pointList.Count; i++) {

			string x = pointList[i][xName].ToString ();
			float y = (System.Convert.ToSingle (pointList[i][yName]) - yMin + 0.5f) / (yMax - yMin);

			Transform cropParent = transform.Find (x);
			if (cropParent == null) {
				GameObject p = new GameObject (x);
				p.transform.parent = transform;
				p.transform.localPosition = new Vector3 (0, 0, 0.2f * i);
				cropParent = p.transform;
			}
			GameObject obj = Instantiate (dataPointPrefab);
			obj.transform.SetParent (cropParent);

			if (xIndex != 1) {
				obj.transform.localPosition = new Vector3 (0.5f * xMin, 0, 0);
				if (xMin == xMax) { xMin = 0; } else { xMin++; }
			}
			obj.transform.DOScale (new Vector3 (0.1f, 0.1f, y * plotScaleY), 4);//latest
			// obj.transform.localScale = new Vector3 (0.1f, 0.1f, y * plotScaleY);

			// // Assigns original values to dataPointName
			string dataPointName =
				inputFile + " (" +
				pointList[i][xName] + ", " +
				pointList[i][yName] + ", " +
				pointList[i][zName] + ")";
			obj.transform.GetChild (0).GetChild (0).GetComponent<Text> ().text = pointList[i][xName].ToString ();
			obj.transform.GetChild (1).GetChild (0).GetComponent<Text> ().text = pointList[i][yName].ToString ();
			obj.transform.GetChild (2).GetChild (0).GetComponent<Text> ().text = pointList[i][zName].ToString ();
			//obj.GetComponentInChildren<Text> ().text = pointList[i][yName].ToString ();
			// Assigns name to the object
			obj.transform.name = dataPointName;
		}

		//Reset positions
		if (xIndex == 1) {
			for (int i = 0; i < transform.childCount; i++) {
				transform.GetChild (i).localPosition = new Vector3 (0, 0, 0.5f * i);
			}
			for (int i = 0; i < pointList.Count; i++) {
				string x = pointList[i][xName].ToString ();
				Transform parent = transform.Find (x);
				for (int j = 0; j < parent.childCount; j++) {
					parent.GetChild (j).localPosition = new Vector3 (0.5f * j, 0, 0);
				}
			}
		}
	}
	// void Start () {
	// 	PlotGraph (1, yield);
	// }

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
	//1vs2+3=6
	public void CostOfCultivationPerHectareVsState () {
		PlotGraph (0, costOfCultivationPerHectare);
	}

	//0vs5
	public void YieldVsCrop () {
		PlotGraph (0, yield);
	}

	//1vs3*5=7
	public void PerHectareCostPriceVsState () {
		PlotGraph (0, perHectareCostPrice);
	}

	//1vs5
	public void YieldVsState () {
		PlotGraph (1, yield);
	}

	//1vs5*100=8
	public void YieldKGVsState () {
		PlotGraph (0, yieldKG);
	}
	void Clear () {
		for (int i = 0; i < transform.childCount; i++) {
			Transform t = transform.GetChild (i);
			Destroy (t.gameObject);
		}
	}
}