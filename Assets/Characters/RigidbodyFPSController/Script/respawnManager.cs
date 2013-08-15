using UnityEngine;
using System.Collections;

[RequireComponent(typeof(HPManager))]

public class respawnManager : MonoBehaviour {
	
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
		if (canRespawn &&
			currentRespawnTime + respawnTime <= Time.time) {
			canRespawn = false;
			transform.position = respawnPosition;
            this.GetComponent<HPManager>().Respawn();
		}
	}
}
