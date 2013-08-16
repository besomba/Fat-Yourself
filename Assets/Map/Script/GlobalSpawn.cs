using UnityEngine;
using System.Collections;

public class GlobalSpawn : MonoBehaviour {
	public Transform playerPrefab;
    public Transform hudPrefab;
	private bool GameLaunch = false;
	private bool CanInitPlayer = false;
    // Use this for initialization
	public void GameBegin() {
		CanInitPlayer = true;
		//Camera.mainCamera.animation.Play("levelAnnimatio");
    }
	
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (!Camera.mainCamera.animation.isPlaying && !GameLaunch && CanInitPlayer) {
			Transform playe = Network.Instantiate(playerPrefab, transform.position, transform.rotation, 0) as Transform;
        	Instantiate(hudPrefab); 
        	GUIManager hud = Object.FindObjectOfType(typeof(GUIManager)) as GUIManager;
        	hud.player = playe.gameObject;
			GameLaunch = true;
		}
	}
}
