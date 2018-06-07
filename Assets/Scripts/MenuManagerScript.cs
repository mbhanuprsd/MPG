using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagerScript : MonoBehaviour {

	public void navigateToSinglePlayer() {
		SceneManager.LoadScene (1);
	}

	public void navigateToMultiplayer() {
		SceneManager.LoadScene (2);
	}

	public void exitGame() {
		Application.Quit ();
	}
}
