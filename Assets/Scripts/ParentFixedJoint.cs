using UnityEngine;
using System.Collections;

//Obsolete code for picking up a sphere which we've disabled in the scene.
[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ParentFixedJoint : MonoBehaviour {

	public Rigidbody rigidBodyAttachPoint;

	SteamVR_TrackedObject trackedObj;
	SteamVR_Controller.Device device;

	FixedJoint fixedJoint;

	void Awake() {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}

	void FixedUpdate () {
		device = SteamVR_Controller.Input ((int)trackedObj.index);
	}

	void OnTriggerStay(Collider col) {
		Debug.Log ("You have collided with " + col.name + " and activated OnTriggerStay");
		if (fixedJoint == null && device.GetTouch (SteamVR_Controller.ButtonMask.Trigger)) {
			fixedJoint = col.gameObject.AddComponent<FixedJoint> ();
			fixedJoint.connectedBody = rigidBodyAttachPoint;
		} else if  (fixedJoint != null && device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger)) {
			GameObject go = fixedJoint.gameObject; //this should be the sphere
			Rigidbody rb = go.GetComponent<Rigidbody>(); //collided object
			Object.Destroy(fixedJoint);
			fixedJoint = null;
			tossObject (rb);
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
