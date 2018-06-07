using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour {

	[SerializeField]
	private string playerName = "Player1";
	[SerializeField]
	private int Health;

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Enemy")
		{
			Destroy(col.gameObject);
			Health -= 10;
		}
	}

	public int getHealth() {
		return Health;
	}

	public string getPlayerName(){
		return playerName;
	}
}
