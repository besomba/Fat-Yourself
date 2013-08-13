using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Gapnel))]
[RequireComponent (typeof (RigidbodyFPSController))]
[RequireComponent (typeof (MainGun))]
[RequireComponent (typeof (MouseLook))]
[RequireComponent (typeof (HPManager))]
[RequireComponent (typeof (MineManager))]


public class InputManager : MonoBehaviour {

    public int mineCoast;
    public int rocketCoast;

    [HideInInspector]
	public RigidbodyFPSController Player;
    [HideInInspector]
	public Gapnel grapnel;
    [HideInInspector]
    public MainGun mainGun;
    [HideInInspector]
    public AnnimatioManager animatioManager;
    [HideInInspector]
    public HPManager hpManager;
    [HideInInspector]
    public MineManager mineManager;

	// Use this for initialization
	void Start () {
		Player = GetComponent<RigidbodyFPSController>();
		grapnel = GetComponent<Gapnel>();
		mainGun = GetComponent<MainGun>();
		animatioManager = GetComponent<AnnimatioManager>();
        hpManager = GetComponent<HPManager>();
        mineManager = GetComponent<MineManager>();
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
			animatioManager.run();
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
			animatioManager.stopJump();
		}
		else
			animatioManager.jump();
	}

    private void mainGunFire()
    {
        if (Input.GetMouseButtonDown(1) && hpManager.GetHP() > rocketCoast * 2 && mainGun.GetCooldown() == 100)
        {
			mainGun.prepareFire();
			
		}
		if (Input.GetMouseButton(1)) {
			mainGun.loadFire();			
		}
        if (Input.GetMouseButtonUp(1) && hpManager.GetHP() > rocketCoast * 2 && mainGun.GetCooldown() == 100)
        {
			mainGun.fireBullet();
			animatioManager.RightFire();
            hpManager.PvChangement(-rocketCoast);
		}
		animatioManager.RightStopFire();
	}

    private void grapnelFire()
    {
		if (Input.GetMouseButtonDown(0)) {
			grapnel.fire();
			animatioManager.leftFire();
		}
		animatioManager.leftStopFire();
		
	}

    private void mineFire()
    {
        if (Input.GetMouseButton(2) && hpManager.GetHP() > mineCoast * 2)
        {
            if (mineManager.Shoot())
                hpManager.PvChangement(-mineCoast);
        }
    }

	void Update () {
		mainGunFire();
		grapnelFire();
        mineFire();
	}
}
