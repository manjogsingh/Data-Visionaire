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
	public DataGraphCostAnalysis costAnalysis;
	// public Growth growthSimulation;

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
			if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Touchpad)) {
				laser.gameObject.SetActive (false);
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
		if (other.gameObject.CompareTag ("Button")) {
			device.TriggerHapticPulse (2000);
			buttonController.ButtonPress (other.name);
			PressButton (other.transform);
		}
	}

	void PressButton (Transform button) {
		string name = button.name;
		switch (name) {
			case "Button1":
				dataPlotter.selectState ("Bihar");
				break;
			case "Button2":
				clustering.Cluster ();
				break;
			case "Button3":
				menuPloter.PlotFile ();
				break;
			case "Button4":
				buttonController.ActivateSubMenu (button);
				break;
			case "Button5":
				costAnalysis.CostOfCultivationPerHectareVsState ();
				break;
			case "Button6":
				costAnalysis.YieldVsCrop ();
				break;
			case "Button7":
				costAnalysis.PerHectareCostPriceVsState ();
				break;
			case "Button8":
				costAnalysis.YieldVsState ();
				break;
			case "Button9":
				costAnalysis.YieldKGVsState ();
				break;
			case "Button10":
			//reset
				break;
			case "Button11":
				break;
			case "Button12":
			//restart
				break;
			case "Arhar":
				menuPloter.DisableRest (name);
				menuPloter.PlotCrop (5, name, 3200, 2200);
				cropSelector.setCrop (name);
				// growthSimulation.Grow(name);
				break;
			case "Cotton":
				menuPloter.DisableRest (name);
				menuPloter.PlotCrop (9, name, 36000, 18000);
				cropSelector.setCrop (name);
				// growthSimulation.Grow(name);
				break;
			case "Gram":
				menuPloter.DisableRest (name);
				menuPloter.PlotCrop (4, name, 9600, 5600);
				cropSelector.setCrop (name);
				// growthSimulation.Grow(name);
				break;
			case "Ground Nuts":
				menuPloter.DisableRest (name);
				menuPloter.PlotCrop (7, name, 9800, 4500);
				cropSelector.setCrop (name);
				// growthSimulation.Grow(name);
				break;
			case "Maize":
				menuPloter.DisableRest (name);
				menuPloter.PlotCrop (2, name, 25000, 14000);
				cropSelector.setCrop (name);
				// growthSimulation.Grow(name);
				break;
			case "Moong":
				menuPloter.DisableRest (name);
				menuPloter.PlotCrop (6, name, 7200, 4500);
				cropSelector.setCrop (name);
				// growthSimulation.Grow(name);
				break;
			case "Rice":
				menuPloter.DisableRest (name);
				menuPloter.PlotCrop (1, name, 110000, 85000);
				cropSelector.setCrop (name);
				// growthSimulation.Grow(name);
				break;
			case "Mustard":
				menuPloter.DisableRest (name);
				menuPloter.PlotCrop (8, name, 8200, 5500);
				cropSelector.setCrop (name);
				// growthSimulation.Grow(name);
				break;
			case "Sugarcane":
				menuPloter.DisableRest (name);
				menuPloter.PlotCrop (10, name, 370000, 270000);
				cropSelector.setCrop (name);
				// growthSimulation.Grow(name);
				break;
			case "Wheat":
				menuPloter.DisableRest (name);
				menuPloter.PlotCrop (3, name, 96000, 68000);
				cropSelector.setCrop (name);
				// growthSimulation.Grow(name);
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