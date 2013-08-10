using UnityEngine;
using System.Collections;

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
    public bool debug = false;

	public void Start () {
        currentPv = defaultPv;
        scaleInitial = this.transform.localScale;
        scaleObjectif = this.transform.localScale;
	}

    public void FixedUpdate()
    {
        if (this.transform.localScale.x != CalculScale(currentPv))
        {
            Rescale();
        }
    }

    public void OnGUI()
    {
        if (debug)
        {
            if (GUI.Button(new Rect(10, 10, 20, 20), "hp--"))
            {
                PvChangement(-10);
            }
            if (GUI.Button(new Rect(35, 10, 20, 20), "hp++"))
            {
                PvChangement(10);
            }
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
        if (currentPv < 0)
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
        scaleObjectif = new Vector3(CalculScale(currentPv), CalculScale(currentPv), CalculScale(currentPv));
    }

    public void die()
    {
        //TODO
    }
}
