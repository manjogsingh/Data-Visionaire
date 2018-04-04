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
	public float plotScaleY=100;
	//public Transform biharTransform, punjabTransform, mumbaiTransform, haryanaTransform, gujratTransform, bhopalTransform,westBengalTransform,uttarPradeshTransform,hyderabadTransform,jaipurTransform;

	// Indices for columns to be assigned
	int year = 0, Rice = 1, Maize = 2, Wheat = 3, Gram = 4, Arhar = 5, Moong = 6, groundNuts = 7, Mustard = 8, Cotton = 9, Sugercane = 10;

	// Full column names
	public string xName;
	public string yName;

	void Awake () {
		pointList = CSVReader.Read (inputFile);
	}

	void PlotCrop (int cropIndex, string lineName) {
		LineRenderer line = transform.Find (lineName).GetComponent<LineRenderer> ();
		line.positionCount = pointList.Count;
		List<string> columnList = new List<string> (pointList[1].Keys);
		// Assign column name from columnList to Name variables
		xName = columnList[year];
		yName = columnList[cropIndex];

		float xMax = 2015f;
		float yMax = 370000f;

		// // Get minimums of each axis
		float xMin = 2005f;
		float yMin = 1000f;

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
}