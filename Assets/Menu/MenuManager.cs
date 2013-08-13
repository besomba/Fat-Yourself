using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {
	
    public string currentMenu;
	public string gameName;
	public string gamePassWord;
	public int gameMaxPlayer = 8;
	private Vector2 ScrollLabel = Vector2.zero;
	

	// Use this for initialization
	void goMenu(string menu) {
		currentMenu = menu;
		gameName = "default";
	}
	
	void mainMenu() {
		if (GUI.Button(new Rect(0,0, 200,50), "Host Game")) {
			goMenu("host");	
		}
		if (GUI.Button(new Rect(0,50, 200,50), "Refresh")) {
			MasterServer.RequestHostList("TeamFight");
		}
	
		GUILayout.BeginArea(new  Rect(Screen.width - 400, 0, 400, Screen.height), "Server List", "Box");
		GUILayout.Space(20);
		foreach (HostData data in MasterServer.PollHostList()) {
			GUILayout.BeginHorizontal("box");
			GUILayout.Label(data.gameName);
			if (GUILayout.Button("Connect")) {
				Network.Connect(data);			
			}
			GUILayout.EndHorizontal();
		}
			
		GUILayout.EndArea();
	
		GUI.Label(new Rect(210,0, 300,50), "Player name");
		NetworkManager.instance.playerName =  GUI.TextField(new Rect(210,50, 100,50), NetworkManager.instance.playerName);
		
	}
	
	void hostMenu() {
		if (GUI.Button(new Rect(0,0, 200,50), "Back")) {
			goMenu("main");
		}
		
		if (GUI.Button(new Rect(0, 50, 200,50), "Start server")) {
			NetworkManager.instance.startServer(gameName, gamePassWord, gameMaxPlayer);

		}
		GUI.Label(new Rect(210,0, 300,50), "server name");
		gameName =  GUI.TextField(new Rect(210,50, 100,50), gameName);
		
		GUI.Label(new Rect(210,100, 300,50), "server password");
		gamePassWord =  GUI.PasswordField(new Rect(210,150, 100,50), gamePassWord, '*');
		
		GUI.Label(new Rect(210,200, 300,50), "server max player");
		gameMaxPlayer = int.Parse( GUI.TextField(new Rect(210,250, 300,50), gameMaxPlayer.ToString()));
		
	}
	
	/*void levelList() {
		if (GUI.Button(new Rect(0,0, 200,50), "Launch Game")) {
		}
		GUI.BeginArea(new  Rect(Screen.width - 400, 0, 400, Screen.height), "Level List", "Box");
		GUILayout.Space(20);
		for(int i = 0; i < NetworkManager.instance.LevelPlayList.Count; i++) {
			GUILayout.BeginHorizontal("box");
			GUILayout.Label(NetworkManager.instance.LevelPlayList[i].mapName);
			if (GUILayout.Button("add"))
				NetworkManager.instance.AddlevelToPlayList(i);
		}
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}*/
	
	void lobyMenu() {
		
		ScrollLabel = 	GUI.BeginScrollView(new Rect(0,0,200,500),Vector2.zero,new Rect(0,0,200,500));
		
		foreach (MPPlayer tmp in NetworkManager.instance.playerList) {
			GUILayout.Box(tmp.playerName);
		}
		GUI.EndScrollView();
		
		GUI.BeginGroup (new Rect(Screen.width - 400, 0, 400, Screen.height), "Map play");
		for(int i = 0; i < NetworkManager.instance.LevelPlayList.Count; i++) {
			GUI.Label(new Rect(0,i*20,200,20), NetworkManager.instance.LevelPlayList[i].mapName);
			if (GUI.Button(new Rect(200,i*20,200,20), "Remove "))
				NetworkManager.instance.RemoveLevelToPlayList(i);
		}
		GUI.EndGroup();
		
		GUI.BeginGroup (new Rect(Screen.width - 850, 0, 400, Screen.height), "Map");
		for(int i = 0; i < NetworkManager.instance.Level.Count; i++) {
			GUI.Label(new Rect(0,i*20,200,20), NetworkManager.instance.Level[i].mapName);
			if (GUI.Button(new Rect(200,i*20,200,20), "Add "))
				NetworkManager.instance.AddlevelToPlayList(i);
		}
		GUI.EndGroup();
		
		if (GUI.Button (new Rect (Screen.width - 100,Screen.height - 50,100,50), "Bottom-right")) {
			 NetworkManager.instance.LaunchGame();
		}
	//	levelList();
		/*if (Network.isServer) {

		
		GUILayout.BeginArea(new  Rect(Screen.width - 400, 0, 400, Screen.height), "Level Play List", "Box");
		GUILayout.Space(20);
		for(int i = 0; i < NetworkManager.instance.LevelPlayList.Count; i++) {
			GUILayout.BeginHorizontal("box");
			GUILayout.Label(NetworkManager.instance.LevelPlayList[i].mapName);
			if (GUILayout.Button("add"))
				NetworkManager.instance.AddlevelToPlayList(i);
			}
			GUILayout.EndHorizontal();
			GUILayout.EndArea();
		}*/
	}
	
    void OnGUI() {
     	if (currentMenu == "main")
			mainMenu();
		if (currentMenu == "host")
			hostMenu();
		if (currentMenu == "loby")
			lobyMenu();
    }

	void Start () {
		currentMenu = "main";
	
	}
	
	void OnConnectedToServer() {
		goMenu("loby");
	}
	
	void OnServerInitialized() {
		goMenu("loby");
	}
	
	// Update is called once per frame
	void Update () {
	}
}
