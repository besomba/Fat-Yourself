using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Gapnel))]
[RequireComponent (typeof (RigidbodyFPSController))]
[RequireComponent (typeof (MainGun))]
[RequireComponent (typeof (MouseLook))]

public class InputManager : MonoBehaviour {
	
	public RigidbodyFPSController Player;
	public Gapnel grapnel;
	public MainGun mainGun;
	public AnnimatioManager animatioManager;

	// Use this for initialization
	void Start () {
		Player = GetComponent<RigidbodyFPSController>();
		grapnel = GetComponent<Gapnel>();
		mainGun = GetComponent<MainGun>();
		animatioManager = GetComponent<AnnimatioManager>();
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
			animatioManager.run();
			Debug.Log("Move");
			Vector3 target =  new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			if (Input.GetButton("Sprint")) {
				Player.Move(true, target);
			}
			else {
				Player.Move(false, target);
			}
		}
		else
			animatioManager.stopRun();
		if (Input.GetButtonDown("Jump")) {
			animatioManager.jump();
			Player.jump();
		}
		if (Player.onTheGround()) {
			Debug.Log("On the ground");
			animatioManager.stopJump();
		}
		else
			animatioManager.jump();
	}
	
	void mainGunFire() {
		if (Input.GetMouseButtonDown(1)) {
			mainGun.prepareFire();
			
		}
		if (Input.GetMouseButton(1)) {
			mainGun.loadFire();			
		}
		if (Input.GetMouseButtonUp(1)) {
			mainGun.fireBullet();
			animatioManager.RightFire();
		}
		animatioManager.RightStopFire();
	}
	
	void grapnelFire(){
		if (Input.GetMouseButtonDown(0)) {
			grapnel.fire();
			animatioManager.leftFire();
		}
		animatioManager.leftStopFire();
		
	}
	
	void Update () {
		mainGunFire();
		grapnelFire();
	}
}
