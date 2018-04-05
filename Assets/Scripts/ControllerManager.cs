using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerManager : MonoBehaviour {

	#region SteamVR variables
	public SteamVR_TrackedObject trackedObject;
	public SteamVR_Controller.Device device;
	#endregion

	#region Teleportation variables
	public LineRenderer laser;
	public GameObject teleportAimer;
	Vector3 teleportLocation;
	public GameObject Player;
	public LayerMask laserMask;
	public float yNudge = 1f;
	public bool isLeftHanded;
	#endregion

	[Header ("Script Refrences")]
	//public Button plot, cluster, menuPlot;
	public DataPlotter dataPlotter;
	public Clustering clustering;
	public CropSelector cropSelector;
	public DataGraphProduction menuPloter;
	public ButtonController buttonController;

	public Toggle Arhar, Cotton, Gram, Groundnut, Maize, Mung, Paddy, Mustard, Sugarcane, Wheat;

	public GameObject India;

	void Start () {
		trackedObject = GetComponent<SteamVR_TrackedObject> ();
	}

	void Update () {
		device = SteamVR_Controller.Input ((int) trackedObject.index);
		if (isLeftHanded) {
			if (device.GetTouch (SteamVR_Controller.ButtonMask.Touchpad)) {

				laser.gameObject.SetActive (true);
				laser.SetPosition (0, gameObject.transform.position);
				RaycastHit hit;

				if (Physics.Raycast (transform.position, transform.forward, out hit, 10, laserMask)) {
					teleportAimer.SetActive (true);
					teleportLocation = hit.point;
					laser.SetPosition (1, teleportLocation);
					teleportAimer.transform.position = new Vector3 (teleportLocation.x, teleportLocation.y + yNudge, teleportLocation.z);
				}
			}

			if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Touchpad)) {
				laser.gameObject.SetActive (false);
				teleportAimer.SetActive (false);
				Player.transform.position = teleportLocation;
			}
		} else {
			if (device.GetTouch (SteamVR_Controller.ButtonMask.Touchpad)) {

				laser.gameObject.SetActive (true);
				laser.SetPosition (0, gameObject.transform.position);
				RaycastHit hit;

				if (Physics.Raycast (transform.position, transform.forward, out hit, 10, laserMask)) {
					teleportLocation = hit.point;
					laser.SetPosition (1, teleportLocation);
				}
			}
		}
	}

	void OnTriggerStay (Collider other) {
		if (other.gameObject.CompareTag ("State")) {
			if (device.GetPressDown (SteamVR_Controller.ButtonMask.Trigger)) {
				GrabObject (other);
			} else if (device.GetPressUp (SteamVR_Controller.ButtonMask.Trigger)) {
				ReleaseObject (other);
			}
		// } else if (other.gameObject.CompareTag ("Button")) {
		// 	if (device.GetPressDown (SteamVR_Controller.ButtonMask.Trigger)) {
		// 		string buttonName = other.gameObject.name;
		// 		switch (buttonName) {
		// 			case "Plot":
		// 				plot.onClick.AddListener (Plot);
		// 				plot.onClick.Invoke ();
		// 				break;
		// 			case "Cluster":
		// 				cluster.onClick.AddListener (Cluster);
		// 				plot.onClick.Invoke ();
		// 				break;
		// 			case "MenuPlot":
		// 				menuPlot.onClick.AddListener (MenuPlot);
		// 				plot.onClick.Invoke ();
		// 				break;
		// 		}
		// 	}
		// } else if (other.gameObject.CompareTag ("Toggle")) {
		// 	if (device.GetPressDown (SteamVR_Controller.ButtonMask.Trigger)) {
		// 		string toggleName = other.gameObject.name;
		// 		cropSelector.setCrop (toggleName);
		// 	}
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag ("UI")) {
			PressButton (other.name);
		}
		else if(other.gameObject.CompareTag("Button")){
			buttonController.ButtonPress(other.name);
		}
	}

	void Plot () {
		dataPlotter.selectState ("Bihar");
	}
	void Cluster () {
		clustering.Cluster ();
	}
	void MenuPlot () {
		menuPloter.PlotFile ();
	}

	void PressButton (string name) {
		switch (name) {
			case "Plot":
				dataPlotter.selectState ("Bihar");
				break;
			case "Cluster":
				clustering.Cluster ();
				break;
			case "Arhar":
				cropSelector.setCrop ("Arhar");
				break;
			case "Cotton":
				cropSelector.setCrop ("Cotton");
				break;
			case "Gram":
				cropSelector.setCrop ("Gram");
				break;
			case "Groundnut":
				cropSelector.setCrop ("Groundnut");
				break;
			case "Maize":
				cropSelector.setCrop ("Maize");
				break;
			case "Mung":
				cropSelector.setCrop ("Mung");
				break;
			case "Paddy":
				cropSelector.setCrop ("Paddy");
				break;
			case "Mustard":
				cropSelector.setCrop ("Mustard");
				break;
			case "Sugarcane":
				cropSelector.setCrop ("Sugarcane");
				break;
			case "Wheat":
				cropSelector.setCrop ("Wheat");
				break;
		}
	}

	Vector3 pos;
	Vector3 defaultPosition {
		get { return pos; }
		set { pos = value; }
	}

	Quaternion rot;
	Quaternion defaultRotation {
		get { return rot; }
		set { rot = value; }
	}

	void GrabObject (Collider other) {
		defaultPosition = other.transform.localPosition;
		defaultRotation = other.transform.localRotation;
		other.transform.SetParent (gameObject.transform);
		other.GetComponent<Rigidbody> ().isKinematic = true;
		device.TriggerHapticPulse (2000);
	}

	void ReleaseObject (Collider other) {
		other.transform.SetParent (India.transform);
		other.GetComponent<Rigidbody> ().isKinematic = false;
		other.transform.localPosition = defaultPosition;
		other.transform.localRotation = defaultRotation;
	}

}