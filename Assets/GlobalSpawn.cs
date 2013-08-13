using UnityEngine;
using System.Collections;

public class GlobalSpawn : MonoBehaviour {
	public Transform playerPrefab;
	// Use this for initialization
	public void GameBegin() {
		Network.Instantiate(playerPrefab, transform.position, transform.rotation, 0);
	}
	
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
