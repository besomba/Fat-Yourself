using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour {
	private bool isLaunch = false;
	public float power;
	public float speed;
	private Vector3 direction;
	private Rigidbody body;

	// Use this for initialization
	
	
	public void launchBullet(float _power, Vector3 dir) {
		power = _power;
		direction = dir;
		isLaunch = true;
		GetComponent<BulletInpact>().explosionPower = power;
	}
	
	void Start () {
		body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isLaunch) {
			body.MovePosition(transform.position + direction * speed * Time.deltaTime);
		}
	}
}
