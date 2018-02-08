using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour {

    #region SteamVR variables
    public SteamVR_TrackedObject trackedObject;
    public SteamVR_Controller.Device device;
    #endregion

    #region Teleportation variables
    public LineRenderer laser;
    public GameObject teleportAimer;
    public Vector3 teleportLocation;
    public GameObject Player;
    public LayerMask laserMask;
    public float yNudge = 1f;
    public bool isLeftHanded;
    #endregion

    public GameObject India;

    void Start () {
        trackedObject = GetComponent<SteamVR_TrackedObject> ();
    }

    void Update () {
        device = SteamVR_Controller.Input ((int) trackedObject.index);
        if (isLeftHanded) {
            if (device.GetTouch (SteamVR_Controller.ButtonMask.Touchpad)) {
                laser.gameObject.SetActive (true);
                teleportAimer.SetActive (true);
                teleportAimer.GetComponent<Animation>().Play();
                
                laser.SetPosition (0, gameObject.transform.position);
                RaycastHit hit;
                if (Physics.Raycast (transform.position, transform.forward, out hit, 10, laserMask)) {
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

        }

    }

    void OnTriggerStay (Collider other) {
        if (other.gameObject.CompareTag ("")) {

            if (device.GetPressDown (SteamVR_Controller.ButtonMask.Trigger)) {
                GrabObject (other);
            } else if (device.GetPressUp (SteamVR_Controller.ButtonMask.Trigger)) {
                ReleaseObject (other);
            }
        }
    }

    Vector3 pos;
    Vector3 defaultPosition {
        get { return pos; }
        set { pos = value; }
    }

    void GrabObject (Collider other) {
        defaultPosition = other.transform.position;
        other.transform.SetParent (gameObject.transform);
        other.GetComponent<Rigidbody> ().isKinematic = true;
        device.TriggerHapticPulse (2000);
    }

    void ReleaseObject (Collider other) {
        other.transform.SetParent (India.transform);
        other.GetComponent<Rigidbody> ().isKinematic = false;
        other.transform.position = defaultPosition;
    }
}