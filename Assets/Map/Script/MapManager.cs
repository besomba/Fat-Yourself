using UnityEngine;
using System.Collections;

public class MapManager : MonoBehaviour {

    public Material[] materials;

	void Start () {
        for (int i = 0; i < this.transform.GetChildCount(); i++)
        {
            int rnd = Random.Range(0, materials.Length);
            this.transform.GetChild(i).renderer.material = materials[rnd];
        }
	}
}
