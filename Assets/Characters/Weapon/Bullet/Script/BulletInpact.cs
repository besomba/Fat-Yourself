using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]

public class BulletInpact : MonoBehaviour
{

    public SphereCollider explosion;
    public float explosionPower;
    public float explosionRadius;

    private bool die;
    private bool hit = false;

    void FixedUpdate()
    {
        if (die)
        {
            Die();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        changeCollider();
        die = true;
    }

    private void changeCollider()
    {
        explosion.enabled = true;
        explosion.isTrigger = true;
        if (explosionRadius == 0)
        {
            explosionRadius = explosionPower / 20;
        }
        explosion.radius = explosionRadius;
    }

    void OnTriggerEnter(Collider other)
    {
        Rigidbody mine = other.gameObject.GetComponent<Rigidbody>();

        if (mine != null)
        {
            if (mine.isKinematic == false)
            {
                mine.AddExplosionForce(explosionPower, this.transform.position, explosionRadius);
            }
        }
        hit = true;
    }

    void Die()
    {
        Destroy(this.gameObject);
        //TODO Drop fat
    }
}
