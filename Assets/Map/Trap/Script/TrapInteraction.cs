using UnityEngine;
using System.Collections;

    [RequireComponent(typeof(Rigidbody))]

public class TrapInteraction : MonoBehaviour {

    public bool isMoving;
    public float timer;
    public float speed;

    private bool inactive = false;

    private float startTime;
    private Vector3 maxScale;
    private Vector3 minScale;
    private bool growUp;
    private bool isStatic;

    void Start()
    {
        if (isMoving == false)
            return;
        maxScale = this.transform.localScale;
        minScale = this.transform.localScale;
        minScale.y = 0.1f;
        growUp = false;
    }

    void FixedUpdate()
    {
        if (isMoving == false)
            return;
        if (isStatic == true && Time.time > startTime + timer)
        {
            startTime = Time.time;
            isStatic = false;
            growUp = !growUp;
        }
        if (isStatic == false)
        {
            if (growUp)
            {
                this.transform.localScale = Vector3.Slerp(minScale, maxScale, (Time.time - startTime) / speed);
                if (maxScale == this.transform.localScale)
                {
                    isStatic = true;
                    startTime = Time.time;
                }
            }
            else
            {
                this.transform.localScale = Vector3.Slerp(maxScale, minScale, (Time.time - startTime) / speed);
                if (minScale == this.transform.localScale)
                {
                    isStatic = true;
                    startTime = Time.time;
                }
            }
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if (inactive == true)
            return;

          if (other.gameObject.tag == "Player")
          {
              Vector3 point =  other.contacts[0].point;

              other.rigidbody.AddExplosionForce(100f, other.contacts[0].point, 1);
              other.gameObject.GetComponent<HPManager>().PvChangement(-10);
          }
    }

    void OnCollisionStay(Collision other)
    {
        if (inactive == true)
            return;

        if (other.gameObject.tag == "Player")
        {
            Vector3 point = other.contacts[0].point;

            other.rigidbody.AddExplosionForce(100f, other.contacts[0].point, 1);
            other.gameObject.GetComponent<HPManager>().PvChangement(-2);
        }
    }

}
