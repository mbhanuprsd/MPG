using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandbyCameras : MonoBehaviour {

	[SerializeField]
	GameObject[] standbyCameras;

	public float timeDelayInSeconds = 5f;

	private float startTime;
	private int camIndex = 0;

	void Start() {
		startTime = Time.time;
		foreach (GameObject cam in standbyCameras) {
			cam.SetActive (false);
		}
		standbyCameras [camIndex].SetActive (true);
	}

	void Update () {
		if (Time.time - startTime > timeDelayInSeconds) {
			toggleCamera ();
		}
	}
		
	private void toggleCamera() {
		startTime = Time.time;
		standbyCameras [camIndex].SetActive (false);
		camIndex = camIndex > standbyCameras.Length - 2 ? 0 : (camIndex + 1);
		standbyCameras [camIndex].SetActive (true);
	}

	void OnGUI() {
		GUILayout.BeginArea (new Rect (Screen.width / 2.0f, Screen.height * 9 / 10, Screen.width, Screen.height / 10));
		GUILayout.Label ("Camera " + (camIndex + 1));
		GUILayout.EndArea ();
	}
}
