using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
	public Transform initialTransform;
	private Transform nextTransform;
	private Transform destinationTranform;

	private ParticleSystem shooting;

	[SerializeField]
	private int speed = 3;

	private GameObject player;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		shooting = GetComponentInChildren<ParticleSystem> ();
	}

	public void setDestination(Transform nextTransform) {
		this.nextTransform = nextTransform;
		destinationTranform = nextTransform;
	}

	void Update ()
	{
		EnemyMovement ();

		OscillateEnemyOnYAxis ();
	}

	private void EnemyMovement() {
		float distance = Vector3.Distance (player.transform.position, transform.position);
		if (distance > 8) {
			MoveRandomly ();
		} else if (distance <= 8 && distance > 4) {
			MoveEnemy (player.transform);
		} else {
			FireAtPlayer ();
		}
	}

	void MoveRandomly ()
	{
		if (Vector3.Distance(transform.position, nextTransform.position) < 1.5f) {
			destinationTranform = initialTransform;
		}

		if (Vector3.Distance(transform.position, initialTransform.position) < 1.5f) {
			destinationTranform = nextTransform;
		}

		MoveEnemy(destinationTranform);
	}
		
	private void MoveEnemy(Transform dest) {
		transform.position = Vector3.MoveTowards (this.transform.position, 
			dest.position, speed * Time.deltaTime);
		transform.LookAt (dest.position);
		if (shooting.isPlaying) {
			shooting.Stop ();
		}
	}

	private void OscillateEnemyOnYAxis ()
	{
		Vector3 pos = transform.position;
		pos.y = 2.0f + 0.3f * Mathf.Sin (2.0f * Time.time);
		transform.position = pos;
	}

	void FireAtPlayer ()
	{
		if (!shooting.isPlaying) {
			shooting.Play ();
		}
	}
}
