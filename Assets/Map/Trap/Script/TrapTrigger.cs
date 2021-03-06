using UnityEngine;
using System.Collections;

public class TrapTrigger : MonoBehaviour {

    public enum Etype
    {
        singleShoot,
        timer
    }

    public Etype myTimer;
    public TrapMouvement target;
    public float timer;

    private float startTime;
    private bool isActive = false;
    private int nb;
    private bool lastGrowUp;

	void Start () {
        target.enabled = false;
        if (myTimer == Etype.timer)
            target.Raz();
	}

    void FixedUpdate()
    {
        if (isActive)
        {
            if (myTimer == Etype.timer && startTime + timer <= Time.time)
            {
                target.enabled = false;
                isActive = false;
            }
            else if (myTimer == Etype.singleShoot && target.getGrowUp() != lastGrowUp && lastGrowUp == false)
            {
                nb++;
                if (nb >= 2)
                {
                    target.enabled = false;
                    isActive = false;
                }
            }
            lastGrowUp = target.getGrowUp();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isActive && other.tag == "Player")
        {
            target.enabled = true;
            target.Raz();
            if (myTimer == Etype.singleShoot)
            {
                target.Raz();
                target.startTime -= target.timerAtMinAndMax;
                nb = 0;
            }
            startTime = Time.time;
            isActive = true;
        }
    }
}
