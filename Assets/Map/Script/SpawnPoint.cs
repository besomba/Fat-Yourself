using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SphereCollider))]

public class SpawnPoint : MonoBehaviour {
	private int currentNumberPlayer = 0;
		
	 void OnTriggerEnter(Collider collision) {
		 if (collision.gameObject.CompareTag("Player"))
			currentNumberPlayer++;
		
	}
	
	 void OnTriggerExit(Collider collision) {
		 if (collision.gameObject.CompareTag("Player"))
			currentNumberPlayer--;
	}
	
	public bool canSpawn() {
		if (currentNumberPlayer == 0)
			return true;
		return false;
	}
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(currentNumberPlayer);	
	}
}
