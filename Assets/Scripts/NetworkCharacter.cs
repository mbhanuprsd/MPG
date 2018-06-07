using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class NetworkCharacter : Photon.MonoBehaviour {

	Vector3 realPosition = Vector3.zero;
	Quaternion realRotation = Quaternion.identity;
	Animator anim;
	bool gotFirstUpdate = false;

	void Start() {
		anim = GetComponent<Animator>();
		if (anim == null) {
			Debug.Log("Forgot to add an animator to the player");
		}
	}

	void Update() {
		if (photonView.isMine) {
			// Handled by character control
		} else {
			transform.position = Vector3.Lerp (transform.position, realPosition, 0.1f);
			transform.rotation = Quaternion.Lerp (transform.rotation, realRotation, 0.1f);
		}
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			stream.SendNext (transform.position);
			stream.SendNext (transform.rotation);
			stream.SendNext(anim.GetFloat("speed"));
			stream.SendNext(anim.GetBool("jump"));
		} else {
			realPosition = (Vector3)stream.ReceiveNext ();
			realRotation = (Quaternion)stream.ReceiveNext ();
			anim.SetFloat("speed", (float)stream.ReceiveNext());
			anim.SetBool("jump", (bool)stream.ReceiveNext());

			if (gotFirstUpdate == false) {
				transform.position = realPosition;
				transform.rotation = realRotation;
				gotFirstUpdate = true;
			}
		}
	}
}
