using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHit : MonoBehaviour {

	public DataGraphProduction production;
	void OnTriggerEnter (Collider other) {
		if (other.CompareTag ("DataPoint")) {
			production.SetValue (other.transform);
		}
	}
}