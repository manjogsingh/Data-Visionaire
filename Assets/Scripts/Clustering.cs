using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using System;

public class Clustering : MonoBehaviour {

	public float radius;
	public GameObject thresholdPoint;
	public GameObject threshold;

	public GameObject westbengal, maharastra, rajasthan;

	Vector3 center;

	public float speed = 4f;
	private float startTime;
	private float journeyLength;

	Collider[] hitColliders;
	List<Vector3> positions = new List<Vector3> ();
	List<Collider> neighbours = new List<Collider> ();

	public void Cluster () {
		thresholdPoint = threshold.GetComponent<Threshold> ().setActiveThreshold ();

		try {
			center = thresholdPoint.transform.position;
			hitColliders = Physics.OverlapSphere (center, radius, 1 << 10);

			foreach (Collider col in hitColliders) {
				//if (col.name != thresholdPoint.name) {
				if (col.name.StartsWith ("West") || col.name.StartsWith ("Mumbai") || col.name.StartsWith ("Jaipur")) {
					neighbours.Add (col);
					positions.Add (col.transform.position);
				}
			}

			westbengal.GetComponent<Renderer> ().material.color = Color.blue;
			maharastra.GetComponent<Renderer> ().material.color = Color.blue;
			rajasthan.GetComponent<Renderer> ().material.color = Color.blue;

			Sequence inOut = DOTween.Sequence ();
			foreach (var col in neighbours) {
				inOut.Insert (0, (col.transform.DOMove (center, speed, false)));
			}

			for (var i = 0; i < positions.Count; i++) {
				inOut.Insert (speed, (neighbours[i].transform.DOMove (positions[i], speed, false)));
				inOut.Insert (speed, (neighbours[i].GetComponent<Renderer> ().material.DOColor (Color.red, speed)));
			}
		} catch (Exception e) {
			Debug.LogException(e);
		}
	}

}