using UnityEngine;
using System.Collections;

public class GraisePoint : MonoBehaviour {
    public int fatValue;

    void Start()
    {
        if (fatValue > 3)
            this.transform.parent.localScale = new Vector3(0.65f, 0.65f, 0.65f);
        else
            this.transform.parent.localScale = new Vector3(0.35f, 0.35f, 0.35f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<HPManager>().PvChangement(fatValue);
            Destroy(this.transform.parent.gameObject);
        }
    }
}
