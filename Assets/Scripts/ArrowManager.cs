using UnityEngine;
using System.Collections;

	//SPAWNING THE ARROWS
	public class ArrowManager : MonoBehaviour {

	public SteamVR_TrackedObject trackedObj;//want a ref to controller (to attach to arrowmanager-telling us where we gonna be palcing thata rrow)

	public static ArrowManager Instance; //..

	private GameObject currentArrow;//ref to whatever the current arrow in our hand will be
	public GameObject arrowPrefab; //ref to arrow prefab we stored in prefab folder 

	public GameObject stringAttachPoint;
	public GameObject arrowStartPoint;
	public GameObject stringStartPoint; //PULLING STRING

	private bool isAttached = false;//PULLING ARROW

	void Awake () {				//..
		if (Instance == null)
			Instance = this;
	}

	void OnDestroy() {
		if (Instance == this)
			Instance = null;

	}

	void Start () {
	
	}
	

	void Update () {
		AttachArrow (); //everytime we create an image for the HMD, update is called..It's making sure we have an arrow atatched to our hand if we dont have an arrow in teh scene.	
		PullString(); //PULLIng STRING-WE CALL PULLSTING AND BELOW IF ITS ATTACHED THEN WE WILL BE MOVING THE STRING
	}

	private void PullString(){//PULLING STRING
		if (isAttached) {
			float dist = (stringStartPoint.transform.position - trackedObj.transform.position).magnitude;//wanna know the diff beween teh string starting pos and where your controller is. Get that distance x some value and pull string back accordingly.
			stringAttachPoint.transform.localPosition = stringStartPoint.transform.localPosition + new Vector3 (5f* dist, 0f, 0f);//now that we have teh distance, we need to update the string
		
			//wheile it is atatched from above codes-if we eveer release wanna call teh fire method
			var device = SteamVR_Controller.Input((int)trackedObj.index);
			if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger)) {
				Fire ();
			}																	                             
		}
	}

	private void Fire(){
		float dist = (stringStartPoint.transform.position - trackedObj.transform.position).magnitude;//...we add this line at end to make arrow go further if pulled back more

		currentArrow.transform.parent = null;//release arrow into wild no parent atatchedmant anymore
		currentArrow.GetComponent<Arrow>().Fired();//..this tells the arrow you been fired, now you can do the updates

		Rigidbody r = currentArrow.GetComponent<Rigidbody> ();
		r.velocity = currentArrow.transform.forward * 30f * dist; //shoot it using phsyics forward direction//..we add this line at end * distance goes with float dis line above to make arrow go further relative to distance
		r.useGravity = true;

		currentArrow.GetComponent<Collider> ().isTrigger = false;//so arrows can hit trees we turn collider trigger off



		//now once arrow show reset to begining positions
		stringAttachPoint.transform.position = stringStartPoint.transform.position;
		currentArrow = null;
		isAttached = false;
	
	}

	public void AttachArrow(){
		if (currentArrow == null) { //if we dont have an arrow in hand
			currentArrow = Instantiate (arrowPrefab);//we will instantiate/call a copy of arrow and saves it with =
			currentArrow.transform.parent = trackedObj.transform;//saying we we jusr created/arrow, wanna make it a child of tracked Obj
			//baiscally doing same thing like goldenbow draggin arrow to controller but in code. Saying lets take on controller and atatch arrow underneath it.
			currentArrow.transform.localPosition = new Vector3 (0f, 0f, 0.342f);//local position is what you see in tranform (not .position which is global-lie a cube in scene parenmted to nothing is in global pos).This line moves arrow right infront of contrller insteaqd of through controller so alignment is better between controllers for shooting. 
			//as controller moves, arrow id 0.342 unitts ahead of controller.
			currentArrow.transform.localRotation = Quaternion.identity;
		}
	}

	public void AttachBowToArrow(){
		currentArrow.transform.parent = stringAttachPoint.transform;
		currentArrow.transform.position = arrowStartPoint.transform.position;//was localPoasition before pulling string
		currentArrow.transform.rotation = arrowStartPoint.transform.rotation;
	
		isAttached = true;//PULLING STRING
	
	}//note string once moved was pulled in left side evenm though arrow middle. needed to drag stringstart and make child of bow. was outside of bow whole time.

}




