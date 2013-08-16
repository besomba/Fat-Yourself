using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public enum Etype
    {
        DeathMatch,
        Defrag
    }

    public Etype gameType;
    public float gameTime;
    public GUISkin skin;

    private bool isDone = false;

    public void GameBegin()
    {
        if (gameType  == Etype.DeathMatch)
        {
            DeathMatch dt = NetworkManager.instance.gameObject.AddComponent<DeathMatch>();
            DeathMatchClient dtm = NetworkManager.instance.gameObject.AddComponent<DeathMatchClient>();
            dt.myId = GetMyId();
            dt.nbPlayers = NetworkManager.instance.gameMaxPlayer;
            dtm.ft = dt;
            dtm.skin = skin;
            isDone = true;
        }
        else if (gameType == Etype.Defrag)
        {
            GlobalSpawn global = FindObjectOfType(typeof(GlobalSpawn)) as GlobalSpawn;
            GameObject player = global.meAsPlayer;
            if (player == null)
            {
                isDone = false;
                return;
            }

            Race dt = NetworkManager.instance.gameObject.AddComponent<Race>();
            RaceClient dtm = player.gameObject.AddComponent<RaceClient>();
            dt.myId = GetMyId();
            dt.nbPlayers = NetworkManager.instance.gameMaxPlayer;
            dtm.ft = dt;
            dtm.skin = skin;
            dt.cl = dtm;
            isDone = true;
        }
    }

    private int GetMyId()
    {
        return int.Parse(NetworkManager.instance.networkView.owner.ToString());
    }

    void Update()
    {
        if (!isDone)
        {
            GameBegin();
        }
    }
}
