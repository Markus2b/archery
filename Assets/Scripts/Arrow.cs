using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	private bool isAttached =  false;

	private bool isFired = false;

	void OnTriggerStay(){
		AttachArrow ();
	}

	void OnTriggerEnter(){//when arrow hit collider of bow it attaches to bow-this line makes that happen/remeber capital T for Trigger here
		//Debug.LogError ("Entered Bow");
		//ArrowManager.Instance.AttachBowToArrow();
		AttachArrow();
	}

	void OnTriggerExit(){
		gameObject.GetComponent<AudioSource>().Play();
	}
	void Update(){//nice arc to arrows now when shot out-will look at the point
		if (isFired && transform.GetComponent<Rigidbody>().velocity.magnitude >5f){//velocity is higher than 5-so stops arrows from dancing on ground once shot like they were without this line or when we set it to 1f
			transform.LookAt (transform.position + transform.GetComponent<Rigidbody> ().velocity);

		}

	}

	public void Fired(){
		isFired = true;

	}

	private void AttachArrow (){//only when touching trigger arrow will get attach to bow
		var device = SteamVR_Controller.Input((int)ArrowManager.Instance.trackedObj.index);//copied line from steam vr test throw and adjusted-we use this to test wehter or not we pulling the trigger
			if (!isAttached && device.GetTouch (SteamVR_Controller.ButtonMask.Trigger)) {
			ArrowManager.Instance.AttachBowToArrow ();
			isAttached = true;
		
		}

	}
}
