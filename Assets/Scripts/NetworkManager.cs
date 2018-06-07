using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{

	public GameObject standbyCamera;
	private SpawnPoint[] spawnPoints;
	private bool connecting = false;

	List<string> chatMessages;
	int maxChatMessages = 5;

	public bool offlineMode = false;

	void Start()
	{
		spawnPoints = FindObjectsOfType<SpawnPoint>();
		PhotonNetwork.player.NickName = PlayerPrefs.GetString("Username", "Jon Snow");
		chatMessages = new List<string>();
	}

	void OnDestroy()
	{
		PlayerPrefs.SetString("Username", PhotonNetwork.player.NickName);
	}
    
	public void AddChatMessage(string s)
	{
		GetComponent<PhotonView>().RPC("AddChatMessage_RPC", PhotonTargets.AllBuffered, s);
	}

	[PunRPC]
	void AddChatMessage_RPC(string s){
		while (chatMessages.Count >= maxChatMessages) {
			chatMessages.RemoveAt(0);
		}
		chatMessages.Add(s);
	}

	void Connect()
	{
		PhotonNetwork.ConnectUsingSettings("MultiFPS v001");      
	}

	void OnGUI()
	{
		GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		GUILayout.EndArea();

		if (!PhotonNetwork.connected && connecting == false)
		{
			GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical();
			GUILayout.FlexibleSpace();

			GUILayout.BeginHorizontal();
			GUILayout.Label("Username: ");
			PhotonNetwork.player.NickName = GUILayout.TextField(PhotonNetwork.player.NickName);
			GUILayout.EndHorizontal();

			if (GUILayout.Button("SinglePlayer"))
			{
				connecting = true;
				PhotonNetwork.offlineMode = true;
				OnJoinedLobby();
			}
			if (GUILayout.Button("MultiPlayer"))
			{
				connecting = true;
				Connect();
			}
			GUILayout.FlexibleSpace();
			GUILayout.EndVertical();
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.EndArea();
		}

		if (PhotonNetwork.connected && connecting == false) {
			GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
			GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

			foreach(string msg in chatMessages){
				GUILayout.Label(msg);
			}

            GUILayout.EndVertical();
            GUILayout.EndArea();
		}
	}

	void OnJoinedLobby()
	{
		Debug.Log("OnJoinedLobby");
		PhotonNetwork.JoinRandomRoom();
	}

	void OnPhotonRandomJoinFailed()
	{
		Debug.Log("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom(null);
	}

	void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom");
		connecting = false;
		SpawnMyPlayer();
	}

	void SpawnMyPlayer()
	{
		AddChatMessage("Spawning player: " + PhotonNetwork.player.NickName);

		if (spawnPoints == null)
		{
			Debug.LogError("No SpawnPonts!!!");
			return;
		}
		SpawnPoint point = spawnPoints[PhotonNetwork.offlineMode ? 0 : Random.Range(0, spawnPoints.Length)];
		GameObject myPlayer = PhotonNetwork.Instantiate("MPlayer", point.transform.position,
			point.transform.rotation, 0);
		myPlayer.GetComponent<MouseLook>().enabled = true;
		myPlayer.GetComponent<PlayerMovement>().enabled = true;
		myPlayer.GetComponent<PlayerShooting>().enabled = true;
		GameObject playerEyes = myPlayer.transform.Find("MainCamera").gameObject;
		playerEyes.SetActive(true);
		playerEyes.GetComponent<MouseLook>().enabled = true;
		standbyCamera.SetActive(false);
	}

	private float RandomFloat()
	{
		return Random.Range(0, 256) / 256.0f;
	}
}
