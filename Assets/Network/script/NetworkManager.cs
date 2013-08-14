using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
 public class MPPlayer {
	public string playerName = "";
	public NetworkPlayer networkPlayer;
}

[System.Serializable]
public class lvlInfo {
	public string mapName;
	public string mapLoadName;
	public Texture maptexture;
} 

public class NetworkManager : MonoBehaviour {
	public static NetworkManager instance;
	public string playerName;
	public bool	inGame = false;
	public int playerMapLoaded = 0;
	public string ip = "127.0.0.1";
	public int port = 4245;
	
	public string gameName;
	public string gamePassWord;
	public int gameMaxPlayer;
	public List<lvlInfo> LevelPlayList = new List<lvlInfo>();
	public List<lvlInfo> Level = new List<lvlInfo>();
	public int currentLevel = 0;
	public List<MPPlayer> playerList = new List<MPPlayer>();
	
	
	
	public void AddlevelToPlayList(int idx) {
		LevelPlayList.Add(Level[idx]);
	}
	
	public void RemoveLevelToPlayList(int idx) {
		LevelPlayList.RemoveAt(idx);
	} 
	
	
	void Start() {
		instance = this;
		DontDestroyOnLoad(this);
	}
	
	void Update() {
		instance = this;
	}
	
	public void startServer(string serverName, string password, int maxUsers) {
		gameName = serverName;
		gamePassWord = password;
		gameMaxPlayer = maxUsers;
		Network.InitializeServer(maxUsers, 4245, false);
		//MasterServer.RegisterHost("TeamFight", gameName, gameName);
		//Network.InitializeSecurity();
	}
	void OnServerInitialized() {
			server_playerJoinRequest(playerName, Network.player);
	}
	
	void OnConnectedToServer() {
		networkView.RPC("server_playerJoinRequest", RPCMode.Server, playerName, Network.player);
	
	}
	
	void OnPlayerDisconnected(NetworkPlayer id) {
		networkView.RPC("Client_removePlayerToList", RPCMode.All, id);
	}
	
	public void LaunchGame() {
		playerMapLoaded = 0;
		if (LevelPlayList.Count > 0) {
			currentLevel = 0;
			networkView.RPC("client_LoadLevel", RPCMode.All, LevelPlayList[0].mapLoadName);
		}
	}
	
	public void LaunchNextMap() {
		currentLevel++;
		playerMapLoaded = 0;
		if (currentLevel <= LevelPlayList.Count)
			currentLevel = 0;
		networkView.RPC("client_LoadLevel", RPCMode.All, LevelPlayList[currentLevel].mapLoadName);
	}
	
	void OnLevelWasLoaded(int id) {
		if (Network.isClient)
			networkView.RPC("server_mapLoaded", RPCMode.Server);
		else
			server_mapLoaded();	
	}
	
	
	[RPC]
	void client_startMap() {
		foreach (GameObject obj in FindSceneObjectsOfType(typeof(GameObject)))
		 obj.SendMessage("GameBegin");
	}
	
	[RPC]
	void server_mapLoaded() {
		Debug.Log("test Mmap load");
		playerMapLoaded++;
		if (playerMapLoaded >= playerList.Count) {
			playerMapLoaded = 0;
			networkView.RPC("client_startMap", RPCMode.All);
		}
	}
	
	[RPC]
	void client_LoadLevel(string levelName) {
		inGame = true;
		Application.LoadLevel(levelName);
	}
	
	[RPC]
	void server_playerJoinRequest(string playerName, NetworkPlayer networkPlayer) {
		networkView.RPC("Client_addPlayerToList", RPCMode.All, playerName, networkPlayer);
		foreach (MPPlayer tmp in playerList) {
			if (tmp.networkPlayer != networkPlayer) {
				networkView.RPC("Client_addPlayerToList", networkPlayer, tmp.playerName, tmp.networkPlayer);
			}
		}
	}
	
	[RPC]
	void Client_addPlayerToList(string playerName, NetworkPlayer networkPlayer) {
		MPPlayer tmp = new MPPlayer();
		tmp.playerName = playerName;
		tmp.networkPlayer = networkPlayer;
		playerList.Add(tmp);
	}
	
	[RPC]
	void Client_removePlayerToList(NetworkPlayer networkPlayer) {
		MPPlayer find = null;

		
		foreach (MPPlayer tmp in playerList) {
			if (tmp.networkPlayer == networkPlayer)
				find = tmp;
		}
		if (find != null) {
			playerList.Remove(find);
		}
	}
}

