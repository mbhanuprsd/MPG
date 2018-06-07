using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{

    public GameObject sniperBulletFXPrefab;

    [PunRPC]
    void SniperBulletFX(Vector3 startPos, Vector3 endPos)
    {
        Debug.Log("Sniper Bullet FX!!");

        if (sniperBulletFXPrefab != null)
        {
            GameObject sniperFX = Instantiate(sniperBulletFXPrefab, startPos, Quaternion.LookRotation(endPos - startPos));
            LineRenderer lineRenderer = sniperFX.transform.Find("LineFX").GetComponent<LineRenderer>();
            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(0, startPos);
                lineRenderer.SetPosition(1, endPos);
            }
        }
    }
}
