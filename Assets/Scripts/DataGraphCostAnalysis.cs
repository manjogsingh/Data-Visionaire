using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphData {
	public float dataPoint;
}

public class DataGraphCostAnalysis : MonoBehaviour {

	private List<Dictionary<string, object>> pointList;
	public string inputFile;
	public GameObject dataPointPrefab;

	public float plotScaleX = 1;
	public float plotScaleY;

	public string xName;
	public string yName;

	float xMax, yMax, xMin, yMin;

	int costOfCultivation = 2;
	int costOfCultivationD = 3;
	int costOfProduction = 4;
	int yield = 5;
	int costOfCultivationPerHectare = 6;
	int perHectareCostPrice = 7;
	int yieldKG = 8;

	void Awake () {
		pointList = CSVReader.Read (inputFile);
	}

	void PlotGraph (int xIndex, int yIndex) {
		List<string> columnList = new List<string> (pointList[1].Keys);

		// Assign column name from columnList to Name variables
		xName = columnList[xIndex];
		yName = columnList[yIndex];

		float xMax = 4;
		float yMax = 30f;

		float xMin = 0;
		float yMin = 0;

		//Loop through Pointlist
		for (var i = 0; i < pointList.Count; i++) {

			string x = pointList[i][xName].ToString ();
			float y = (System.Convert.ToSingle (pointList[i][yName]) - yMin) / (yMax - yMin);

			Transform parent = transform.Find (x);

			GameObject obj = Instantiate (dataPointPrefab);
			obj.transform.SetParent (parent);

			if (xIndex != 1) {
				obj.transform.localPosition = new Vector3 (.3f * xMin, 0, 0);
				if (xMin == xMax) { xMin = 0; } else { xMin++; }
			}

			obj.transform.localScale = new Vector3 (0.1f, 0.1f, y * plotScaleY);

			// // Assigns original values to dataPointName
			string dataPointName =
				inputFile + " (" +
				pointList[i][xName] + ", " +
				pointList[i][yName] + ")";

			// Assigns name to the object
			obj.transform.name = dataPointName;
		}

		//Reset positions
		if (xIndex == 1) {
			for (int i = 0; i < pointList.Count; i++) {
				string x = pointList[i][xName].ToString ();
				Transform parent = transform.Find (x);
				for (int j = 0; j < parent.childCount; j++) {
					parent.GetChild (j).localPosition = new Vector3 (0, 0, .3f * j);
				}
			}
		}
	}
	void Start () {
		PlotGraph (1, yield);
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
}