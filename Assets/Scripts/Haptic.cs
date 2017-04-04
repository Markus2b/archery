using UnityEngine;
using System.Collections;
using System;


[RequireComponent(typeof(SteamVR_TrackedObject))]// rewuires system abobe
public class Haptic : MonoBehaviour {

	SteamVR_TrackedObject trackedObj;
	SteamVR_Controller.Device device;


	void Awake () {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();

	}
	
	// Update is called once per frame
	void Update ()
	{
		device = SteamVR_Controller.Input ((int)trackedObj.index);
	}

	void OnTriggerStay (Collider col)
		{
		Debug.Log ("You have collided with " + col.name);
		device.TriggerHapticPulse();
		}

}
