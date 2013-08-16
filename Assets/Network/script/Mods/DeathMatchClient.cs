using UnityEngine;
using System.Collections;

public class DeathMatchClient : MonoBehaviour {

    public DeathMatch ft;

    HPManager target;

    private float lastPv;
    public bool scoreDisplay = false;

    public GUISkin skin;

    private bool isLock;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            scoreDisplay = true;
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            scoreDisplay = false;
        }
    }

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

    public void OnGUI()
    {
        if (scoreDisplay)
        {
            GUI.skin = skin;
            GUI.BeginGroup(new Rect((int)(Screen.width - (Screen.width / 3.5)), (int)(Screen.height / 20), Screen.width / 4, 50), skin.customStyles[1]);
            GUI.Label(new Rect(25, 5, 300, 50), "Timer: " + ((int)ft.TimeRemaning).ToString());
            GUI.EndGroup();

            GUI.BeginGroup(new Rect((int)(Screen.width - (Screen.width / 3.5)), (int)(Screen.height / 6), Screen.width / 4, 40 * (NetworkManager.instance.playerList.Count + 1)), skin.customStyles[1]);
            GUI.Label(new Rect(25, 10, 300, 50), "Death Rank: ");


            NetworkManager tmpNetManager = GetComponent<NetworkManager>();
            for (int i = 0; i < ft.playerDeath.Count || i >= 8; i++)
            {
                int rank = i + 1;
                GUI.Button(new Rect(20, (i + 1) * 40, 200, 50), rank.ToString(), skin.customStyles[2]);
                GUI.Label(new Rect(50, (i + 1) * 40, 200, 50), ft.playerDeath[i].player.playerName, skin.customStyles[2]);
                GUI.Label(new Rect(220, (i + 1) * 40, 200, 50), ft.playerDeath[i].nbDeath.ToString(), skin.customStyles[2]);
            }
            GUI.EndGroup();
        }
    }

    public void changementOnDisplay()
    {
        if (!isLock)
        {
            scoreDisplay = !scoreDisplay;
        }
    }

    public void lockDisplay()
    {
        scoreDisplay = true;
        target.gameObject.SetActive(false);
    }

}

