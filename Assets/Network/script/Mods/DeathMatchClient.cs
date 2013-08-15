using UnityEngine;
using System.Collections;

public class DeathMatchClient : MonoBehaviour {

    public DeathMatch ft;

    HPManager target;

    private float lastPv;

    void Start () {
        Debug.Log("HERO !");
      
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (target == null)
        {
            target = Object.FindObjectOfType(typeof(HPManager)) as HPManager;
            ft = Object.FindObjectOfType(typeof(DeathMatch)) as DeathMatch;
        }
        else
        {
            float tmpPv = target.GetHP();
            if (tmpPv == 0 && lastPv != 0)
            {
                ft.ImDead();
            }
            lastPv = tmpPv;
        }
	}
}
