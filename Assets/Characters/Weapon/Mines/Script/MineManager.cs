using UnityEngine;
using System.Collections;

public class MineManager : MonoBehaviour {

    public GameObject mine;
    public float cooldown;

    private float startTime;

    public bool Shoot()
    {
        if (Time.time > startTime + cooldown)
        {
            Instantiate(mine, this.transform.position, this.transform.rotation);
            startTime = Time.time;
            return true;
        }
        return false;
    }
}
