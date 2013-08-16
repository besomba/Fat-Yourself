using UnityEngine;
using System.Collections;

public class GlobalSpawn : MonoBehaviour {
	public Transform playerPrefab;
    public Transform hudPrefab;
	public Transform playerSave;
	private bool GameLaunch = false;
	private bool CanInitPlayer = false;
    // Use this for initialization
	public void GameBegin() {
			Transform playe = Network.Instantiate(playerPrefab, transform.position, transform.rotation, 0) as Transform;
			playerSave = playe;
        	Instantiate(hudPrefab); 
        	GUIManager hud = Object.FindObjectOfType(typeof(GUIManager)) as GUIManager;
        	hud.player = playe.gameObject;
			GameLaunch = true;
    }
	
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}
}
