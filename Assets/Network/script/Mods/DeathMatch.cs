using UnityEngine;
using System.Collections;

public class DeathMatch : MonoBehaviour {

    public float TimeRemaning;
    public bool isGameOver = false;
    public int[] playerDeath;
    public int myId;
    public int nbPlayers;

    void Start()
    {
        Debug.Log("I need a");

        playerDeath = new int[nbPlayers + 1]; 
    }

    void FixedUpdate()
    {
        TimeRemaning -= Time.deltaTime;
    }

    [RPC]
    void PlayerDead(int idPLayer)
    {
        Debug.Log("Player " + idPLayer.ToString() + " is dead");
        playerDeath[idPLayer] += 1;
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
        Debug.Log("Noooooooooooooooooooooooooooooooo !");
        networkView.RPC("PlayerDead", RPCMode.All, myId);
    }
}
