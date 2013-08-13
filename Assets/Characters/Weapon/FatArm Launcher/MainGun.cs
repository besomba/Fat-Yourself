using UnityEngine;
using System.Collections;

public class MainGun : MonoBehaviour {
	public float minPower;
	public float maxPower;
	public float powerGrowRate;
	private float currentTime;
	private float currentPower;
	
	public Transform raycastView;
	public Transform gun;
	public Transform bulletPrefab;
	private Transform currentBullet;
	
	public float fireRate;
	private float currentFireRate = 0;
	private bool fire = false;
	// Use this for initialization
	void Start () {
		currentPower = minPower;
		currentTime = -fireRate;
	}

    public float GetCooldown()
    {
        float ret = (Time.time - currentFireRate) * 100 / (fireRate);
        if (ret > 100)
            ret = 100;
        return ret;
    }

	public void prepareFire() {
		if (currentFireRate + fireRate <= Time.time) {
		currentTime = Time.time;
		fire = true;
		}
	}
	
	public void loadFire() {
		if (fire) {
			if (currentTime + 1 < Time.time) {
				currentPower += powerGrowRate;
				if (currentPower > maxPower)
					currentPower = maxPower;
			}
		}
	}
	
	public void fireBullet() {
		if (fire) {
			RaycastHit hit;
			fire = false;
			currentFireRate = Time.time;
			currentBullet =  Instantiate(bulletPrefab, gun.position, gun.rotation) as Transform;
            if (Physics.Raycast(raycastView.position, raycastView.TransformDirection(Vector3.forward), out hit)) {
				Vector3 dir =  hit.point - gun.position;
				currentBullet.GetComponent<BulletManager>().launchBullet(currentPower, dir.normalized);
			}
			else {
				currentBullet.GetComponent<BulletManager>().launchBullet(currentPower, raycastView.TransformDirection(Vector3.forward));
			}
			currentPower = minPower;
		}		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay(raycastView.position, raycastView.TransformDirection(Vector3.forward) * 50, Color.green);
		/*if (Input.GetMouseButtonDown(1) && currentFireRate + fireRate < Time.time) {
			currentTime = Time.time;
			fire = true;
		}
		if (Input.GetMouseButton(1) && fire) {
			if (currentTime + 1 < Time.time) {
				currentPower += powerGrowRate;
				if (currentPower > maxPower)
					currentPower = maxPower;
			}
		}
		if (Input.GetMouseButtonUp(1) && fire) {
			RaycastHit hit;
			fire = false;
			currentFireRate = Time.time;
			currentBullet =  Instantiate(bulletPrefab, gun.position, gun.rotation) as Transform;
			if (Physics.Raycast(raycastView.position, raycastView.TransformDirection(Vector3.forward), out hit)) {
				Vector3 dir =  hit.point - gun.position;
				currentBullet.GetComponent<BulletManager>().launchBullet(currentPower, dir.normalized);
			}
			else {
				currentBullet.GetComponent<BulletManager>().launchBullet(currentPower, raycastView.TransformDirection(Vector3.forward));
			}
			currentPower = minPower;
		}*/
	}
}
