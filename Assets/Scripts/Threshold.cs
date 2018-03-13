using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Threshold : MonoBehaviour {

	private List<Dictionary<string, object>> pointList;
	public string inputFile;
	public GameObject dataPointPrefab;
	public CropSelector cropSelector;
	public float plotScale = 8;

	// Indices for columns to be assigned
	public int columnX = 0;
	public int columnY = 1;
	public int columnZ = 2;
	public int columnW = 3;

	// Full column names
	public string xName;
	public string yName;
	public string zName;
	public string wName;

	//float xMax = 2015f;
	float yMax = 30f;
	float zMax = 80f;
	float wMax = 1800f;

	//float xMin = 2005;
	float yMin = 20f;
	float zMin = 30f;
	float wMin = 300f;

	void Start () {
		inputFile = "Thresholds";
		pointList = CSVReader.Read (inputFile);
		List<string> columnList = new List<string> (pointList[1].Keys);

		// Assign column name from columnList to Name variables
		xName = columnList[columnX];
		yName = columnList[columnY];
		zName = columnList[columnZ];
		wName = columnList[columnW];
	}

	public GameObject setActiveThreshold () {
		for (var i = 0; i < pointList.Count; i++) {
			if (pointList[i][xName].Equals (cropSelector.SelectedCrop())) {
				float y = (System.Convert.ToSingle (pointList[i][yName]) - yMin) / (yMax - yMin);
				float z = (System.Convert.ToSingle (pointList[i][zName]) - zMin) / (zMax - zMin);
				float w = (System.Convert.ToSingle (pointList[i][wName]) - wMin) / (wMax - wMin);

				GameObject obj = Instantiate (dataPointPrefab, new Vector3 (y, z, w) * plotScale, Quaternion.identity);
				obj.transform.SetParent (this.transform);

				// Assigns original values to dataPointName
				string dataPointName =
					"Crop: " + pointList[i][xName] + "\n(" +
					"Temprature: " + pointList[i][yName] + ", " +
					"Humidity: " + pointList[i][zName] + ", " +
					"Rainfall: " + pointList[i][wName] + ")";

				// Assigns name to the prefab
				obj.transform.name = dataPointName;
				obj.GetComponent<PointValue> ().setValue (dataPointName);
				// Gets material color and sets it to a new RGB color we define
				obj.GetComponent<Renderer> ().material.color = new Color (y, z, w, 1.0f);
				return obj;
			}
		}
		return null;
	}

}