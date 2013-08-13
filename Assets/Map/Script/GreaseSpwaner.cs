using UnityEngine;
using System.Collections;

public class GreaseSpwaner : MonoBehaviour {
    public GameObject grease;
    public float cooldown;

    private float startTime;
    private bool currentGrease;
    private bool stay;

    public int greaseValue;

	// Use this for initialization
	void Start () {
    	currentGrease = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (stay == false && startTime + cooldown <= Time.time)
        {
            GameObject myGrease = Instantiate(grease, this.transform.position, this.transform.rotation) as GameObject;
            myGrease.GetComponentInChildren<GraisePoint>().fatValue = greaseValue;
            currentGrease = true;
            stay = true;
        }
        else
        {
            stay = false;
        }
	}

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Grease")
        {
            stay = true;
            startTime = Time.time;
        }
    }
}
