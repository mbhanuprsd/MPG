using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    public float fireRate = 0.5f;
    private float cooldown = 0;
    public float damage = 25f;
    FXManager fXManager;

    void Start()
    {
        fXManager = FindObjectOfType<FXManager>();
        if (fXManager == null) {
            Debug.LogError("No FX Manager");
        }
    }

    void Update()
    {
        cooldown -= Time.deltaTime;

        if (Input.GetButton("Fire1"))
        {
            Fire();
        }
    }

    private void Fire()
    {
        if (cooldown > 0)
        {
            return;
        }
        Debug.Log("Fired!!");

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Vector3 hitPoint;

        Transform hitTransform = FindClosestHitInfo(ray, out hitPoint);

        if (hitTransform != null)
        {
            Debug.Log("We hit: " + hitTransform.name);

            Health health = hitTransform.GetComponent<Health>();

            while (health == null && hitTransform.parent)
            {
                hitTransform = hitTransform.parent;
                health = hitTransform.GetComponent<Health>();
            }

            if (health != null)
            {
                PhotonView photonView = health.GetComponent<PhotonView>();
                if (photonView == null)
                {
                    Debug.LogError("No photonView!");
                }
                else
                {
                    health.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.AllBuffered, damage);
                }
            }

            if (fXManager != null) {
                fXManager.GetComponent<PhotonView>().RPC("SniperBulletFX", PhotonTargets.All, Camera.main.transform.position, hitPoint);
            }
        }
        else {
            if (fXManager != null)
            {
                hitPoint = Camera.main.transform.position + (Camera.main.transform.forward * 100f);
                fXManager.GetComponent<PhotonView>().RPC("SniperBulletFX", PhotonTargets.All, Camera.main.transform.position, hitPoint);
            }
        }

        cooldown = fireRate;
    }

    private Transform FindClosestHitInfo(Ray ray, out Vector3 hitPoint)
    {
        RaycastHit[] hits = Physics.RaycastAll(ray);
        Transform closestHit = null;
        hitPoint = Vector3.zero;
        float distance = 0;

        foreach (RaycastHit hit in hits)
        {
            if (hit.transform != this.transform && (closestHit == null || hit.distance < distance))
            {
                closestHit = hit.transform;
                hitPoint = hit.point;
                distance = hit.distance;
            }
        }

        return closestHit;
    }
}
