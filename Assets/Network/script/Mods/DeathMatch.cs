using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeathMatch : MonoBehaviour {

    public struct stat
    {
        public int id;
        public int nbDeath;
        public MPPlayer player;
    }

    public float TimeRemaning = 120;
    public bool isGameOver = false;
    public List<stat> playerDeath;
    public int myId;
    public int nbPlayers;
    public DeathMatchClient cl;

    private bool waitEnd = false;

    void Start()
    {
        playerDeath = new List<stat>(nbPlayers);

        for (int i = 0; i < NetworkManager.instance.playerList.Count; i++)
        {
            stat current = new stat();

            current.id = i;
            current.player = NetworkManager.instance.playerList[i];
            current.nbDeath = 3;
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

    private static int CompareStat(stat x, stat y)
    {
       return x.nbDeath.CompareTo(y.nbDeath);
    }

    [RPC]
    void WaitEnd()
    {
        cl.lockDisplay();
    }

    [RPC]
    void PlayerDead(int idPLayer)
    {
        for (int i = 0; i < playerDeath.Count; i++)
        {
            if (playerDeath[i].id == idPLayer)
            {
                stat tmp = playerDeath[i];
                tmp.nbDeath++;
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

    public void ImDead()
    {
        networkView.RPC("PlayerDead", RPCMode.All, myId);
    }
}
