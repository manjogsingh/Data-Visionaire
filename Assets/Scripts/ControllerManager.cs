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

	#region Swipe
	float swipeSum;
	float touchLast;
	float touchCurrent;
	float distance;
	bool hasSwipedLeft;
	bool hasSwipedRight;
	public GameObject yearRotation;
	#endregion

	#region UI Elements
	public Text stateName;
	public Text stateRainfall;
	public Text stateYear;
	public GameObject spot;
	public GameObject data;
	#endregion
	[Header("UI")]
	public DataPlotter dataPlotter;
	public Clustering clustering;
	public CropSelector cropSelector;

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

					if (hit.transform.gameObject.layer == 8) {
						teleportAimer.SetActive (true);
						spot.SetActive (false);
						teleportLocation = hit.point;
						laser.SetPosition (1, teleportLocation);
						teleportAimer.transform.position = new Vector3 (teleportLocation.x, teleportLocation.y + yNudge, teleportLocation.z);
					} else if (hit.transform.gameObject.layer == 9) {
						teleportAimer.SetActive (false);
						spot.SetActive (true);
						laser.SetPosition (1, hit.point);
						spot.transform.position = hit.point;
						stateName.text = hit.transform.name;
						stateYear.text = yearRotation.GetComponent<MenuRotation> ().GetYear ().ToString ();
						string x = stateRainfall.text + " " + data.GetComponent<DataParser> ().GetRainfallValues (stateName.text, stateYear.text);
						stateRainfall.text = x;
					}
				}
			}

			if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Touchpad)) {
				laser.gameObject.SetActive (false);
				teleportAimer.SetActive (false);
				spot.SetActive (false);
				Player.transform.position = teleportLocation;
			}
		} else {
			if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Touchpad)) {
				yearRotation.SetActive (true);
				touchLast = device.GetAxis (Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
			}

			if (device.GetTouch (SteamVR_Controller.ButtonMask.Touchpad)) {
				touchCurrent = device.GetAxis (Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
				distance = touchCurrent - touchLast;
				touchLast = touchCurrent;
				swipeSum += distance;
				if (!hasSwipedRight) {
					if (swipeSum > 0.3f) {
						swipeSum = 0;
						SwipeRight ();
						hasSwipedRight = true;
						hasSwipedLeft = false;
					}
				}
				if (!hasSwipedLeft) {
					if (swipeSum < -0.3f) {
						swipeSum = 0;
						SwipeLeft ();
						hasSwipedLeft = true;
						hasSwipedRight = false;
					}
				}
			}

			if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Touchpad)) {
				swipeSum = 0;
				touchCurrent = 0;
				touchLast = 0;
				hasSwipedLeft = false;
				hasSwipedRight = false;
				yearRotation.SetActive (false);
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
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag ("UI")) {
			PressButton (other.name);
		}
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

	void SwipeLeft () {
		yearRotation.GetComponent<MenuRotation> ().MenuLeft ();
	}

	void SwipeRight () {
		yearRotation.GetComponent<MenuRotation> ().MenuRight ();
	}
}