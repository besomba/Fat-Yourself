using UnityEngine;
using System.Collections;

public class respawnManager : MonoBehaviour {
	
	public bool testRespawn = false;
	public float respawnTime = 5;
	private float currentRespawnTime = 0;
	private bool canRespawn = false;
	private Vector3 respawnPosition;
	
	public void respawn() {
		foreach (GameObject spawnPoint in GameObject.FindGameObjectsWithTag("SpawnPoint")) {
			if (spawnPoint.GetComponent<SpawnPoint>() != null &&
				spawnPoint.GetComponent<SpawnPoint>().canSpawn()) {
				currentRespawnTime = Time.time;
				canRespawn = true;
				respawnPosition = spawnPoint.transform.position;
			}
		}
	}	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (testRespawn) {
			testRespawn = false;
			respawn();
		}
		if (canRespawn &&
			currentRespawnTime + respawnTime <= Time.time) {
			canRespawn = false;
			transform.position = respawnPosition;
		}
	}
}
