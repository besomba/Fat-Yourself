using UnityEngine;
using System.Collections;

public class RaceClient : MonoBehaviour {


    public Race ft;

    HPManager target;

    private float lastPv;
    public bool scoreDisplay = false;

    public GUISkin skin;

    private bool isLock;

    private float myTimer;
    private float myLastTime = 999f;
    void Start()
    {
        myTimer = Time.time;
    }

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

    void FixedUpdate()
    {
        if (ft == null)
        {
            ft = Object.FindObjectOfType(typeof(Race)) as Race;
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
            GUI.Label(new Rect(25, 10, 300, 50), "Timer Rank: ");


            NetworkManager tmpNetManager = GetComponent<NetworkManager>();
            for (int i = 0; i < ft.playerDeath.Count || i >= 8; i++)
            {
                int rank = i + 1;
                GUI.Button(new Rect(20, (i + 1) * 40, 200, 50), rank.ToString(), skin.customStyles[2]);
                GUI.Label(new Rect(50, (i + 1) * 40, 200, 50), ft.playerDeath[i].player.playerName, skin.customStyles[2]);
                GUI.Label(new Rect(180, (i + 1) * 40, 200, 50), ft.playerDeath[i].time.ToString(), skin.customStyles[2]);
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "RaceFinal")
        {
            float thisTime = ((int)((Time.time - myTimer) * 100)) / 100f;
            if (myLastTime > thisTime)
            {
                ft.ParkourOver(thisTime);
                myLastTime = thisTime;
            }
            myTimer = Time.time + 5f;
        }
    }
}
