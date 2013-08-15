using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(respawnManager))]

public class HPManager : MonoBehaviour {

    //HP
    public int defaultPv = 100;
    public int minPv = 30;
    public int maxPv = 200;
    private int currentPv;

    //HP SCALE ANIMATION
    private Vector3 scaleObjectif;
    private Vector3 scaleInitial;
    public float scaleSpeed;
    public float startTime;

    //DEBUG
    public bool isRepawn = true;

	public void Start () {
        currentPv = defaultPv;
        scaleInitial = this.transform.localScale;
        scaleObjectif = this.transform.localScale;
        PvChangement(0);
	}

    public int GetHP()
    {
        return currentPv;
    }

    public void FixedUpdate()
    {
        if (this.transform.localScale.x != CalculScale(currentPv))
        {
            Rescale();
        }
        if (currentPv <= 0)
        {
            currentPv = 0;
            rigidbody.velocity = Vector3.zero;
        }
    }

    private float CalculScale(int hp)
    {
        float retValue;

        retValue = (defaultPv + ((hp - defaultPv) / 2f)) / 100;
        return (retValue);
    }

    private void Rescale()
    {
        this.transform.localScale = Vector3.Slerp(scaleInitial, scaleObjectif, (Time.time - startTime) / scaleSpeed);
    }

    public void PvChangement(int effect)
    {
        int prevHp = currentPv;
        currentPv += effect;
        if (currentPv <= 0)
        {
            die();
            return;
        }
        else if (currentPv > maxPv)
        {
            currentPv = maxPv;
        }
        scaleInitial = this.transform.localScale;
        startTime = Time.time;
        rigidbody.mass = CalculScale(currentPv);
        scaleObjectif = new Vector3(CalculScale(currentPv), CalculScale(currentPv), CalculScale(currentPv));
    }

    public void die()
    {
        if (!isRepawn)
            return;
        isRepawn = false;
        currentPv = 0;
        rigidbody.velocity = Vector3.zero;
        this.GetComponent<respawnManager>().respawn();
    }

    public void Respawn()
    {
        currentPv = defaultPv;
        PvChangement(0);
        isRepawn = true;
    }
}
