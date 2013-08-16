using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Race : MonoBehaviour {

    public struct statRace
    {
        public int id;
        public float time;
        public MPPlayer player;
    }

    public float TimeRemaning = 120;
    public bool isGameOver = false;
    public List<statRace> playerDeath;
    public int myId;
    public int nbPlayers;
    public RaceClient cl;

    private bool waitEnd = false;

    void Start()
    {
        playerDeath = new List<statRace>(nbPlayers);

        for (int i = 0; i < NetworkManager.instance.playerList.Count; i++)
        {
            statRace current = new statRace();

            current.id = i;
            current.player = NetworkManager.instance.playerList[i];
            current.time = 99f;
            playerDeath.Add(current);
        }
    }

    void FixedUpdate()
    {
        if (Network.isServer)
        {
            if (!waitEnd)
            {
                TimeRemaning -= Time.deltaTime;
                networkView.RPC("UpdateTimer", RPCMode.All, TimeRemaning);
                if (TimeRemaning <= 0)
                {
                    TimeRemaning = 2f;
                    networkView.RPC("WaitEnd", RPCMode.All);
                }
            }
            else
            {
                TimeRemaning -= Time.deltaTime;
                if (TimeRemaning <= 0)
                {
                    NetworkManager.instance.LaunchNextMap();
                }
            }
        }
    }

    private static int CompareStat(statRace x, statRace y)
    {
        return x.time.CompareTo(y.time);
    }

    [RPC]
    void WaitEnd()
    {
        cl.lockDisplay();
    }

    [RPC]
    void PlayerComplete(int idPLayer, float time)
    {
        for (int i = 0; i < playerDeath.Count; i++)
        {
            if (playerDeath[i].id == idPLayer)
            {
                statRace tmp = playerDeath[i];
                tmp.time = time;
                playerDeath[i] = tmp;
                playerDeath.Sort(CompareStat);
                return;
            }
        }
    }

    [RPC]
    void UpdateTimer(float timer)
    {
        TimeRemaning = timer;
    }

    [RPC]
    void EndGame()
    {
        isGameOver = true;
    }

    public void ParkourOver(float ttime)
    {
        networkView.RPC("PlayerComplete", RPCMode.All, myId, ttime);
        cl.GetComponent<HPManager>().die();

    }
}
