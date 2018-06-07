using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

    public float selfDestructTime = 1.0f;

private void Awake()
    {
        //Debug.LogWarning("Awake");
    }

    private void OnEnable()
    {
        //Debug.LogWarning("OnEnable");
    }

    private void Start()
    {
        //Debug.LogWarning("Start");
    }

    void Update () {
        //Debug.LogWarning("Update");
        selfDestructTime -= Time.deltaTime;

        if (selfDestructTime <= 0) {
            PhotonView photonView = GetComponent<PhotonView>();

            if (photonView != null && photonView.instantiationId != 0) {
                PhotonNetwork.Destroy(gameObject);
            } else {
                Destroy(gameObject);
            }
        }
	}

    private void LateUpdate()
    {
       // Debug.LogWarning("LateUpdate");
    }

    private void OnGUI()
    {
       // Debug.LogWarning("OnGUI");
    }

    private void OnApplicationQuit()
    {
       // Debug.LogWarning("OnApplicationQuit");
    }

    private void OnDisable()
    {
       // Debug.LogWarning("OnDisable");
    }

    private void OnDestroy()
    {
       // Debug.LogWarning("OnDestry");
    }
}
