using UnityEngine;
using System.Collections;

public class MapManager : MonoBehaviour {

    public Material[] materials;

	void Start () {
        MaterialsEffect(this.transform);
	}

    private void MaterialsEffect(Transform obj)
    {
        for (int i = 0; i < obj.GetChildCount(); i++)
        {
            int rnd = Random.Range(0, materials.Length);
            if (obj.GetChild(i).renderer == null)
            {
                MaterialsEffect( obj.GetChild(i).transform);
            }
            else if (obj.GetChild(i).tag == "Map")
            {
                obj.GetChild(i).renderer.material = materials[rnd];
            }
        }

    }
}
