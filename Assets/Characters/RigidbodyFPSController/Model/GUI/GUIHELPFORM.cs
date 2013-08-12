using UnityEngine;
using System.Collections;

public class GUIHELPFORM : MonoBehaviour {
	 
	protected void OnDrawGizmos() {
		Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 5, Color.red);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
