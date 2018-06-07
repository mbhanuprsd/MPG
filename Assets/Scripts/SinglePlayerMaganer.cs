using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlayerMaganer : MonoBehaviour {

	[SerializeField]
	private Text playerNameText;
	[SerializeField]
	private Text playerScoreText;
	[SerializeField]
	private Text playerHealthText;
	[SerializeField]
	private Transform[] spawnPoints;
	[SerializeField]
	private GameObject enemyPrefab;

	private int Score;
	private GameObject player;
	private PlayerInfo playerInfo;

	// Use this for initialization
	void Start () {
		Score = 0;
		player = GameObject.FindGameObjectWithTag ("Player");
		playerInfo = player.GetComponent<PlayerInfo> ();

		for (int i = 0; i < spawnPoints.Length; i++) {
			InstantiateEnemy (i);
		}
	}
	
	// Update is called once per frame
	void Update () {
		playerNameText.text = "Player: " + playerInfo.getPlayerName ();
		playerHealthText.text = "Health: " + playerInfo.getHealth ();
		playerScoreText.text = "Score: " + Score;

//		if (Time.time - instatiatedTime >= enemySpawnInterval) {
//			instatiatedTime = Time.time;
//			InstantiateEnemy ();
//		}
	}

	private void InstantiateEnemy(int spawnIndex) {
		//		int spawnIndex = Random.Range (0, spawnPoints.Length);
		Transform spawnPoint = spawnPoints [spawnIndex];
		GameObject enemy = Instantiate (enemyPrefab, spawnPoint);
		enemy.GetComponent<EnemyScript> ().initialTransform = spawnPoint;

		Transform destination = spawnPoints [spawnIndex < spawnPoints.Length - 1 
			? spawnIndex + 1 : 0];
		enemy.GetComponent<EnemyScript> ().setDestination(destination);
		enemy.transform.parent = null;
	}
}
