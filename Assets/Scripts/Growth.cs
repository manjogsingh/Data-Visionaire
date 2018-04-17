using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growth : MonoBehaviour {

	// Rainfall intensity particle syatem simulation speed 700=.5 750=1 800=1.5 900=2 1000=2.5 1400=3
	public struct RainValues {
		public const float rice = 1.5f;
		public const float maize = 0.5f;
		public const float wheat = 1.5f;
		public const float gram = 2f;
		public const float arhar = 2.5f;
		public const float moong = 1f;
		public const float groundNuts = 3f;
		public const float mustard = 1.5f;
		public const float cotton = 3f;
		public const float sugarcane = 2f;
	}

	public ParticleSystem rain;
	public Terrain cropTerrain;
	public TerrainData cropTerrainData;

	private void Awake()
	{
		foreach (TreeInstance tree in cropTerrainData.treeInstances) {
			TreeInstance t = tree;
			t.heightScale = 0f;
		}
	}
	public void Grow (string cropName) {
		var main = rain.main;
		rain.Play ();
		StartCoroutine (GrowCrop ());
		switch (cropName) {
			case "Arhar":
				main.simulationSpeed = RainValues.arhar;
				break;
			case "Cotton":
				main.simulationSpeed = RainValues.cotton;
				break;
			case "Gram":
				main.simulationSpeed = RainValues.gram;
				break;
			case "Ground Nuts":
				main.simulationSpeed = RainValues.groundNuts;
				break;
			case "Maize":
				main.simulationSpeed = RainValues.maize;
				break;
			case "Moong":
				main.simulationSpeed = RainValues.moong;
				break;
			case "Rice":
				main.simulationSpeed = RainValues.rice;
				break;
			case "Mustard":
				main.simulationSpeed = RainValues.mustard;
				break;
			case "Sugercane":
				main.simulationSpeed = RainValues.sugarcane;
				break;
			case "Wheat":
				main.simulationSpeed = RainValues.wheat;
				break;
			default:
				rain.Stop ();
				break;
		}
	}
	public void ResetComponent () {
		rain.Stop ();
	}

	IEnumerator GrowCrop () {
		foreach (TreeInstance tree in cropTerrainData.treeInstances) {
			TreeInstance t = tree;
			t.heightScale = 1f;
		}
		yield return null;
	}
}