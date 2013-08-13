using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

    public GameObject player;

    private MineManager mineManager;
    private MainGun gunManager;
    private Gapnel gapnelManager;
    private HPManager hpManager;

    public GameObject coolDownMine;
    public GameObject coolDownHook;
    public GameObject coolDownLauncher;

    public GameObject logoMine;
    public GameObject logoLauncher;
    public GameObject logoHook;

    public GameObject HPLogo;
    public GameObject center;

    private float lastMineValue;
    private float lastHookValue;
    private float lastLaunchereValue;
    private float lastHPValue;

    public float sizeMax;
	
	// Update is called once per frame

    void Start()
    {
        if (player != null)
        {
            mineManager = player.GetComponent<MineManager>();
            gunManager = player.GetComponent<MainGun>();
            gapnelManager = player.GetComponent<Gapnel>();
            hpManager = player.GetComponent<HPManager>();
        }
    }

    void ResizeGameObject(float nextValue, GameObject target)
    {
        target.transform.parent.localPosition = new Vector3(nextValue / 200f, target.transform.parent.localPosition.y, target.transform.parent.localPosition.z);
        target.transform.localScale = new Vector3(nextValue / 100f, target.transform.localScale.y, target.transform.localScale.z);
    }

	void FixedUpdate () {
        if (lastHPValue != hpManager.GetHP())
        {
            lastHPValue = hpManager.GetHP();
            HPLogo.transform.localScale = new Vector3(lastHPValue / 100f, lastHPValue / 100f, HPLogo.transform.localScale.z);
        }

        if (lastMineValue != mineManager.GetCooldown())
        {
            lastMineValue = mineManager.GetCooldown();
            ResizeGameObject(lastMineValue, coolDownMine);
            if (lastMineValue == 100)
            {
                logoMine.SetActive(true);
            }
            else
            {
                logoMine.SetActive(false);
            }
        }
        if (lastLaunchereValue != gunManager.GetCooldown())
        {
            lastLaunchereValue = gunManager.GetCooldown();
            ResizeGameObject(lastLaunchereValue, coolDownLauncher);
            if (lastLaunchereValue == 100)
            {
                logoLauncher.SetActive(true);
            }
            else
            {
                logoLauncher.SetActive(false);
            }
        }
        if (lastHookValue != gapnelManager.GetCooldown())
        {
            lastHookValue = gapnelManager.GetCooldown();
            ResizeGameObject(lastHookValue, coolDownHook);
            if (lastHookValue == 100)
            {
                logoHook.SetActive(true);
            }
            else
            {
                logoHook.SetActive(false);
            }

        }
        if (gapnelManager.inRange())
        {
            center.renderer.material.color = Color.green;
        }
        else
        {
            center.renderer.material.color = Color.red;
        }
	}
}
