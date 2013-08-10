using UnityEngine;
using System.Collections;

public class Gapnel : MonoBehaviour {

	// Use this for initialization
	public Rigidbody parentRigidbody;
	public float grabDistance = 5;
	public float gradPower = 50;
	public Transform rayCastSource;
	public bool isCurrentWeapon = true;
	private RaycastHit hit;
	public float fireRate = 1;
	private float saveTime;
	
	void Start () {
		parentRigidbody =  GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay(rayCastSource.position, rayCastSource.TransformDirection(Vector3.forward) * grabDistance, Color.red);
		if (Input.GetMouseButtonDown(0) && Time.time > saveTime + fireRate) {
			saveTime = Time.time;
					if (Physics.Raycast(rayCastSource.position,
			rayCastSource.TransformDirection(Vector3.forward),
			out hit,
			grabDistance)) {
				GetComponent<RigidbodyFPSController>().currentNumberOfWallJump = 0;
				Vector3 dir;
				dir = hit.point - transform.position;
				dir = dir.normalized * gradPower;
				parentRigidbody.AddForce(dir, ForceMode.Impulse);
			}
		}
	}
}
