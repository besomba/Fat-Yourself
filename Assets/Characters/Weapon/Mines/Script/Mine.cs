using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mine : MonoBehaviour {

    public float timer;
    public float explosionPower;
    public SphereCollider collider;

    public ParticleSystem particles;
    public GameObject grease;

    private float initialTime;
    private bool annimationPlayed = false;
    public float explosionRadius;
    private bool explosionDone = false;
    private bool isActive = false;

    public float triggerCoolDown = 0.5f;
    private float startTimeTrigger;

    private bool isTriggered = false;
	// Use this for initialization

    private List<GameObject> hited;

	void Start () {
        initialTime = Time.time;
        particles.Stop();

        if (collider == null)
            collider = GetComponent<SphereCollider>();
        if (particles == null)
            particles = GetComponent<ParticleSystem>();

        hited = new List<GameObject>();
	}
	
	void FixedUpdate () {
        if (initialTime + timer <= Time.time)
        {
            if (isActive == false)
            {
                Activation();
            }
            else if (explosionDone && annimationPlayed == false)
            {
                Explosion();
            }
            else if (explosionDone == true && annimationPlayed == true)
            {
                GameObject tmp = (GameObject)Instantiate(grease, this.transform.position, this.transform.rotation);
                GraisePoint mp = tmp.GetComponentInChildren<GraisePoint>();
                mp.fatValue = 5;
                Destroy(this.transform.parent.gameObject);
            }
        }
	}

    private void MyTrigger(Collider other)
    {
        if (!isTriggered)
        {
            if (other.tag == "Player")
            {
                isTriggered = true;
                startTimeTrigger = Time.time;
            }
            else
            {
                return;
            }
        }

        if (startTimeTrigger + triggerCoolDown <= Time.time)
        {
            return;
        }

        Rigidbody mine = other.gameObject.GetComponent<Rigidbody>();

        if (mine != null)
        {
            if (mine.tag == "Player" || mine.tag == "Mine" || mine.tag == "DynamicTrap")
            {
                foreach (GameObject ie in hited)
                {
                    if (ie == mine.gameObject)
                        return;
                }
                hited.Add(other.gameObject);
                mine.AddExplosionForce(explosionPower, this.transform.position, explosionRadius);
            }
        }
        explosionDone = true;
    }

    void OnTriggerEnter(Collider other)
    {
        MyTrigger(other);
    }

    void OnTriggerStay(Collider other)
    {
        MyTrigger(other);
    }

    private void Activation()
    {
        collider.enabled = true;
        isActive = true;
    }

    private void Explosion()
    {
        collider.radius = explosionRadius;
        initialTime = Time.time;
        annimationPlayed = true;
        for (int i = 0; i < this.transform.GetChildCount(); i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
        timer = 0.2f;
        particles.Play();
    }
}
