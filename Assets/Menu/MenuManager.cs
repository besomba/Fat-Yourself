using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {
	
    public string currentMenu;
	public GUISkin skin;
	public Texture Image;
	public string gameName;
	public string gamePassWord;
	public int gameMaxPlayer = 8;
	string searchServer = "";
	
	public int currentMinServer = 0;
	public int currentMaxServer = 5;
	
	public int currentPlayerMin = 0;
	public int currentPlayerMax = 5;
	
	public int currentPlayListMin = 0;
	public int currentPlayListMax = 5;
	
	public int currentLevelListMin = 0;
	public int currentLevelListMax = 5;
	
	
	private Vector2 ScrollLabel = Vector2.zero;
	

	// Use this for initialization
	void goMenu(string menu) {
		currentMenu = menu;
		gameName = "default";
	}
	
	int getNumberOfServer() {
		int i = 0;
		foreach (HostData data in MasterServer.PollHostList()) {
			i++;
		}
		return i;
	}
	
	void ConnectMenu() {
		GUI.BeginGroup (new Rect(Screen.width - 800, 150, 800,Screen.height - 150));
		if (GUI.Button(new Rect(0,0, 200,50), "Back")) {
			goMenu("main");	
		}
		if (GUI.Button(new Rect(0,50, 200,50), "Refresh")) {
			MasterServer.RequestHostList("TeamFight");
		}
			GUI.BeginGroup (new Rect(250, 0, 550,450));
			GUI.Label(new Rect(0,0,200,50), "Search name :");
			searchServer = GUI.TextField(new Rect(200,0,200,50), searchServer);
			int i = 1;
			if (GUI.Button(new Rect(500, 50, 50,50), "/|\\")) {
				if (currentMinServer > 0) {
					currentMaxServer--;
					currentMinServer--;
				}
			}
			if (GUI.Button(new Rect(500, 250, 50,50), "\\|/")) {
				if (currentMaxServer < getNumberOfServer()) {
					currentMaxServer++;
					currentMinServer++;
				}
			}
			foreach (HostData data in MasterServer.PollHostList()) {
				if (i - 1 >= currentMinServer && i - 1 < currentMaxServer) {
					GUI.Label(new Rect(0,i*50,200,50), data.gameName);
					GUI.Label(new Rect(200,i*50,100,50), data.connectedPlayers + "/" + data.playerLimit);
					
					if (GUI.Button(new Rect(300,i*50,200,50), "Connect")) {
						Network.Connect(data);
					}
				}
				i++;
			}
			GUI.EndGroup();
			GUI.Label(new Rect(250,350,200,50), "server IP :");
			NetworkManager.instance.ip = GUI.TextField(new Rect(450,350,150,50), NetworkManager.instance.ip);
			GUI.Label(new Rect(250,400,200,50), "server port :");
			NetworkManager.instance.port = int.Parse(GUI.TextField(new Rect(450,400,150,50), NetworkManager.instance.port.ToString()));
			if (GUI.Button(new Rect(650,400,150,50), "Connect")) {
				Network.Connect(NetworkManager.instance.ip, NetworkManager.instance.port);
			}
		GUI.EndGroup();
	}
	
	void mainMenu() {
			
		GUI.Label(new Rect(100,100, 300,50), "Welcome " + NetworkManager.instance.playerName); 
		GUI.BeginGroup (new Rect(Screen.width - 300, 300, 300,300));

		if (GUI.Button(new Rect(50,0, 200,50), "Play Game")) {
			goMenu("connect");	
		}
		if (GUI.Button(new Rect(50,50, 200,50), "Host Game")) {
			goMenu("host");	
		}
		if (GUI.Button(new Rect(50,100, 200,50), "settings")) {
			goMenu("host");	
		}
		GUI.EndGroup();
		
/*		if (GUI.Button(new Rect(0,0, 200,50), "Host Game")) {
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
	
		GUI.Label(new Rect(210,0, 200,50), "Player name");
		NetworkManager.instance.playerName =  GUI.TextField(new Rect(210,50, 200,50), NetworkManager.instance.playerName, 10);*/
		
	}
	
	void hostMenu() {
		GUI.BeginGroup (new Rect(Screen.width - 700, 150, 700,Screen.height - 150));
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
			if (GUI.Button(new Rect(210,250, 50,50), "-")) {
								gameMaxPlayer -= 2;
			if (gameMaxPlayer < 2) {
				gameMaxPlayer = 2;
			}	
			}
			GUI.Label(new Rect(310,250, 50,50), gameMaxPlayer.ToString());
			if (GUI.Button(new Rect(410, 250, 50,50), "+")) {
					gameMaxPlayer += 2;
				if (gameMaxPlayer > 32) {
					gameMaxPlayer = 32;
				}
			}
		GUI.EndGroup();
		
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
			
		GUI.BeginGroup (new Rect(Screen.width - 200, 100, 200,450));
		GUI.Label(new Rect(0,0,200,50), "Player List");
		int p = 1;
		if (GUI.Button(new Rect(150, 50, 50,50), "/|\\")) {
			if (currentPlayerMin > 0) {
				currentPlayerMin--;
				currentPlayerMax--;
			}
		}
		if (GUI.Button(new Rect(150, 250, 50,50), "\\|/")) {
			if (currentPlayerMax < NetworkManager.instance.playerList.Count) {
				currentPlayerMax++;
				currentPlayerMin++;
			}
		}
		foreach (MPPlayer tmp in NetworkManager.instance.playerList) {
			Debug.Log(p);
			if (p - 1 >= currentPlayerMin && p - 1 < currentPlayerMax) {
				Debug.Log("pass");
				GUI.Label(new Rect(0,(p - currentPlayerMin) * 50,150,50), tmp.playerName);
			}
			p++;
		}
		GUI.EndGroup();
		
		if (Network.isServer) { 
			GUI.BeginGroup (new Rect(0, 100, 300,450));
			GUI.Label(new Rect(0,0,300,50), "Level PlayList");
			int pl = 1;
			if (GUI.Button(new Rect(250, 50, 50,50), "/|\\")) {
				if (currentPlayListMin > 0) {
					currentPlayListMin--;
					currentPlayListMax--;
				}
			}
			if (GUI.Button(new Rect(250, 250, 50,50), "\\|/")) {
				if (currentPlayListMax < NetworkManager.instance.LevelPlayList.Count) {
					currentPlayListMax++;
					currentPlayListMin++;
				}
			}
			foreach (lvlInfo tmp in NetworkManager.instance.LevelPlayList) {
				Debug.Log(pl);
				if (pl - 1 >= currentPlayListMin && pl - 1 < currentPlayListMax) {
					Debug.Log("pass");
					string map = tmp.mapName;
					GUI.Label(new Rect(0,(pl - currentPlayListMin) * 50,200,50), tmp.mapName, GUI.skin.customStyles[2]);
					if (GUI.Button(new Rect(200,(pl - currentPlayListMin)*50,50,50), "-"))
						NetworkManager.instance.RemoveLevelToPlayList(pl - 1);
				}
				pl++;
			}
			GUI.EndGroup();
			
			
			GUI.BeginGroup (new Rect(350, 100, 300,450));
			GUI.Label(new Rect(0,0,300,50), "Level list");
			int l = 1;
			if (GUI.Button(new Rect(250, 50, 50,50), "/|\\")) {
				if (currentLevelListMin > 0) {
					currentLevelListMin--;
					currentLevelListMax--;
				}
			}
			if (GUI.Button(new Rect(250, 250, 50,50), "\\|/")) {
				if (currentLevelListMax < NetworkManager.instance.Level.Count) {
					currentLevelListMax++;
					currentLevelListMin++;
				}
			}
			foreach (lvlInfo tmp in NetworkManager.instance.Level) {
				Debug.Log(pl);
				if (l - 1 >= currentLevelListMin && l - 1 < currentLevelListMax) {
					Debug.Log("pass");
					string map = tmp.mapName;
					GUI.Label(new Rect(0,(l - currentLevelListMin) * 50,200,50), tmp.mapName, GUI.skin.customStyles[2]);
					if (GUI.Button(new Rect(200, (l - currentLevelListMin)*50,50,50), "-"))
						NetworkManager.instance.AddlevelToPlayList(l - 1);
				}
				l++;
			}
			GUI.EndGroup();
			
			
			if (GUI.Button (new Rect (Screen.width - 250,Screen.height - 100,200,50), "Launch Game")) {
				 NetworkManager.instance.LaunchGame();
			}
		}
		else {
			GUI.Label(new Rect(0, 100, 450,450), "Waiting for Other Player.");
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
	
	void LoginMenu() {
		
		GUI.BeginGroup (new Rect(Screen.width - 300, 150, 300,300), skin.customStyles[1]);
		GUI.Label(new Rect(50,100, 200,50), "Player name");
		NetworkManager.instance.playerName =  GUI.TextField(new Rect(50,150, 200,50), NetworkManager.instance.playerName, 10);
		if (GUI.Button(new Rect(50,225, 200,50), "Ok")) {
			goMenu("main");	
		}
		GUI.EndGroup();
	}
	
    void OnGUI() {
		GUI.BeginGroup (new Rect(0,0, Screen.width, Screen.height), Image);
		GUI.Label(new Rect(0,0, Screen.width,100), "FAT YOURSLEF", skin.customStyles[0]); 
		GUI.skin = skin;
		if (currentMenu == "login")
			LoginMenu();
     	if (currentMenu == "main")
			mainMenu();
		if (currentMenu == "host")
			hostMenu();
		if (currentMenu == "loby")
			lobyMenu();
		if (currentMenu == "connect")
			ConnectMenu();
		GUI.EndGroup();
    }

	void Start () {
		currentMenu = "login";
	
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
