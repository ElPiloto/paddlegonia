using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class PickupParent : MonoBehaviour {
	
	SteamVR_TrackedObject trackedObj;
	SteamVR_Controller.Device device;

	//this adds a property under the left controller's Pickup Parent script
	//under the "Inspector" pane
	public Transform sphere;

	GameObject paddle;

	// Use this for initialization
	//Changed Start() to Awake(), lookup:
	// docs.unity3d.com/Manual/ExecutionOrder.html
	void Awake () {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
		paddle = this.transform.FindChild("Paddle").gameObject;
		//paddle = (GameObject)Instantiate (Resources.Load ("P2"));
//		paddle = GameObject.CreatePrimitive (PrimitiveType.Cylinder);
//		paddle.transform.position = trackedObj.transform.position;
//		paddle.transform.localScale = new Vector3 (0.05f, 0.25f, 0.05f);
//		paddle.transform.Rotate(new Vector3(90f,0f,0f));
//		paddle.transform.parent = trackedObj.transform;
//		paddle.active = false;
	}
	
	//Again: modified Update to FixedUpdate
	//FixedUpdate called on every step of Physics engine
	//called 50 times per second, normally, but we mopdified
	//the fixedupdate time in our unity project
	void FixedUpdate () {
		device = SteamVR_Controller.Input ((int)trackedObj.index);
		//these next two ifs disable or enable the paddle gameObject based
		// on whether or not the trigger is down
		if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
			paddle.SetActive (true);
		}
		//
		if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)) {
			paddle.SetActive (false);
		}
		//leaving the below just for reference
//		if (device.GetTouch (SteamVR_Controller.ButtonMask.Trigger)) {
//			Debug.Log ("BOUT TO TRIGGER");
//		}
//
//		if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger)) {
//			
//			Debug.Log ("TRIGGERED!!!");
//		}
//
//		if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger)) {
//			Debug.Log ("SAFE SPACE");
//		}
//
//		if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger)) {
//			Debug.Log ("HOLDING PRESS");
//		}
//

//
//		if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad)) {
//			sphere.transform.position = Vector3.zero;
//			sphere.GetComponent<Rigidbody> ().velocity = Vector3.zero;
//			sphere.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
//		}
			
	}
	//Obsolete code for picking up sphere that is currently disabled in scene
	void OnTriggerStay(Collider col) {
		Debug.Log ("You have collided with " + col.name + " and activated OnTriggerStay");
		if (device.GetTouch (SteamVR_Controller.ButtonMask.Trigger)) {
			Debug.Log ("You have collided with " + col.name + " while holding down touch");
			col.attachedRigidbody.isKinematic = true;
			col.gameObject.transform.SetParent (this.gameObject.transform);
		}

		if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger)) {
			Debug.Log ("You have collided with " + col.name + " and released the touch!");
			col.gameObject.transform.SetParent(null);
			col.attachedRigidbody.isKinematic = false;

			tossObject(col.attachedRigidbody);
		}
	}

	void tossObject(Rigidbody rb) {
		Transform origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
		if (origin != null) {
			rb.velocity = origin.TransformVector (device.velocity);
			rb.angularVelocity = origin.TransformVector (device.angularVelocity);
		} else {
			rb.velocity = device.velocity;
			rb.angularVelocity = device.angularVelocity;
		}
	}


}
