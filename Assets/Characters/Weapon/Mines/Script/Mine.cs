using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour {

    public float timer;
    public float explosionPower;
    public SphereCollider collider;

    public ParticleSystem particles;

    private float initialTime;
    private bool isEnd = false;
    private bool isDead = false;
    private bool annimationPlayed = false;
    public float explosionRadius;
    private bool explosionDone = false;
    private bool isActive = false;

    public float triggerCoolDown = 0.5f;
    private float startTimeTrigger;

    private bool isTriggered = false;
	// Use this for initialization
	void Start () {
        initialTime = Time.time;
        particles.Stop();

        if (collider == null)
            collider = GetComponent<SphereCollider>();
        if (particles == null)
            particles = GetComponent<ParticleSystem>();
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
                Destroy(this.transform.parent.gameObject);
            }
        }
	}

    void OnTriggerEnter(Collider other)
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
            if (mine.tag == "Player" || mine.tag == "Mine" || mine.tag == "DynamicTrap" )
            {
                Debug.Log("Pock");
                mine.AddExplosionForce(explosionPower, this.transform.position, explosionRadius);
            }
        }
        explosionDone = true;
    }

    void OnTriggerStay(Collider other)
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

        if (mine != null && other.gameObject != this.transform.parent.gameObject)
        {
            if (mine.tag == "Player" || mine.tag == "Mine" || mine.tag == "DynamicTrap")
            {
                Debug.Log("Pock");
                mine.AddExplosionForce(explosionPower, this.transform.position, explosionRadius);
            }
        }
        explosionDone = true;
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
