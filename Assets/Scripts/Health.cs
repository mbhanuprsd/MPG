using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	[SerializeField]
	private float hitPoints = 100;

	private float currentHitPoints;

	void Start()
	{
		currentHitPoints = hitPoints;
	}

    [PunRPC]
	public void TakeDamage(float amt)
	{
		currentHitPoints -= amt;
		if (currentHitPoints <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		if (GetComponent<PhotonView>().instantiationId == 0)
		{
			Destroy(gameObject);
		} else {
			if (GetComponent<PhotonView>().isMine) {
				PhotonNetwork.Destroy(gameObject);
			}
		}
	}
}
