using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]

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

	// Use this for initialization
	void Start () {
        initialTime = Time.time;
        collider.isTrigger = false;
        collider.enabled = true;
        particles.Stop();
	}
	
	void FixedUpdate () {
        if (initialTime + timer <= Time.time)
        {
            if (collider.isTrigger == false)
            {
                Activation();
                Debug.Log("Activation");
            }
            else if (annimationPlayed == false)
            {
                Explosion();
                Debug.Log("Explosion");
                initialTime = Time.time;
                annimationPlayed = true;
                for (int i = 0; i < this.transform.GetChildCount(); i++)
                {
                    this.transform.GetChild(i).gameObject.SetActive(false);
                }
                timer = 0.2f;
            }
            else if (annimationPlayed == true)
            {
                Debug.Log("Destroy");
                Destroy(this.gameObject);
            }
        }
        if (explosionDone)
        {
            DisableMine();
        }
        else if (annimationPlayed)
        {
            explosionDone = true;
        }
	}

    void OnTriggerEnter(Collider other)
    {
        Rigidbody mine = other.gameObject.GetComponent<Rigidbody>();

        if (mine != null)
        {
            if (mine.isKinematic == false)
            {
                Debug.Log("Pock");
                mine.AddExplosionForce(explosionPower, this.transform.position, explosionRadius);
            }
        }
        explosionDone = true;
    }

    private void Activation()
    {
        collider.isTrigger = true;
        collider.enabled = true;
        collider.radius = explosionRadius;
    }

    private void DisableMine()
    {
        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;
        collider.enabled = false;
    }

    private void Explosion()
    {
        particles.Play();
    }
}
