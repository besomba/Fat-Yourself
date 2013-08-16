using UnityEngine;
using System.Collections;

public class Hook : MonoBehaviour {
	
	public Transform playerowner;
	
	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerEnter() {
		playerowner.GetComponent<Gapnel>().fire();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
