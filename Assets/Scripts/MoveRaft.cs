using UnityEngine;
using System.Collections;

//This script is basically obsolete since we are moving the boat in Paddler.cs
public class MoveRaft : MonoBehaviour {

	Vector3 translationVector;

	// Use this for initialization
	void Start () {
		translationVector = new Vector3 (0f, 0f, 0.02f);
		//uncomment to get closer to edge of river bank
		//TODO: parametrize this value in relation to left and right bank x transform values
		//this.gameObject.transform.position = new Vector3 (-20f, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		//this.gameObject.transform.Translate (this.translationVector, Space.World);
		//Debug.Log (gameObject.transform.position);
	}
}
