using UnityEngine;
using System.Collections;

//NOTE: this component has an invisible (disabled mesh renderer) cube that is used for
//detecting when the paddle is underwater
public class Paddler : MonoBehaviour {

	//this needs to get a reference to our CameraRig's parent gameObject's transform
	//so that we can move it around.  Currently that is called "Drifter"
	public Transform boat;

	//this shows up in the unity "inspector" interface for this component so we can
	//set it using the GUI
	public float speed = 1;

	Vector3 translationVector;

	// Use this for initialization
	void Start () {
		//takes in speed defined in "Inspector"
		translationVector = new Vector3 (0f, 0f, 0.1f*speed);
	}

	//Current behavior simply moves the boat whenever we collide with a box underneath
	//the boat
	void OnTriggerStay(Collider col) {
		Debug.Log ("COLLISION!!!");

		//move both our boat and our cube collision detector (this moves both because
		//the collision detector is a child of the boat gameObject)
		boat.gameObject.transform.Translate (this.translationVector, Space.World);
	}
}
