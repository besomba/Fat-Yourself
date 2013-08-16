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

    public void GameBegin()
    {
        if (gameType  == Etype.DeathMatch)
        {
            GameObject player = FindObjectOfType(typeof(RigidbodyFPSController)) as GameObject;
            DeathMatch dt = NetworkManager.instance.gameObject.AddComponent<DeathMatch>();
            DeathMatchClient dtm = NetworkManager.instance.gameObject.AddComponent<DeathMatchClient>();
            dt.myId = GetMyId();
            dt.nbPlayers = NetworkManager.instance.gameMaxPlayer;
            dtm.ft = dt;
            dtm.skin = skin;
        }
        else if (gameType == Etype.Defrag)
        {
            GameObject player = FindObjectOfType(typeof(RigidbodyFPSController)) as GameObject;
            DeathMatch dt = this.gameObject.AddComponent<DeathMatch>();
            DeathMatchClient dtm = player.AddComponent<DeathMatchClient>();
            dt.myId = GetMyId();
            dt.nbPlayers = GetComponent<NetworkManager>().gameMaxPlayer;
            dtm.ft = dt;
        }
    }

    private int GetMyId()
    {
        return int.Parse(NetworkManager.instance.networkView.owner.ToString());
    }
}
