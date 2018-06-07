using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatingcamera : MonoBehaviour {

	[SerializeField]
	Transform centerObject;
	[SerializeField]
	int speed;

	void Update () {
		transform.RotateAround(centerObject.position, Vector3.up, speed * Time.deltaTime);
	}
}
