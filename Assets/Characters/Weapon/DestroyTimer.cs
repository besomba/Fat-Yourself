using UnityEngine;
using System.Collections;

public class DestroyTimer : MonoBehaviour {

    public float coolDown;
    private float startTime;

    void Start () {
        startTime = Time.time;
	}
	
	void FixedUpdate () {
        if (Time.time >= startTime + coolDown)
        {
            Destroy(this.gameObject);
        }
	}
}
