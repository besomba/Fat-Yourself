using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
	
	public Transform position;
	private bool positionSet = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	public void setPosition(Transform pos) {
		position =  pos;
		positionSet  = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (positionSet)  {
			transform.position = position.position;
			transform.rotation = position.rotation;
		}
	}
}
