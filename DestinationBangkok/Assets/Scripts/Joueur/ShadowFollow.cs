using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Auteur : Raphaël Jeudy
public class ShadowFollow : MonoBehaviour
{
    public GameObject player;

    // Update est appelée des dizaines de fois/seconde
    void Update()
    {
        float distanceSol = Mathf.Clamp(player.GetComponent<PlayerController>().infoDecal.distance, 1, 1.75f);
        
        transform.position = new Vector3(player.transform.position.x, Mathf.Clamp(player.GetComponent<PlayerController>().infoDecal.point.y - 0.04f, 39, 100), player.transform.position.z);
        transform.localScale = new Vector3(1 / (distanceSol), 1 / (distanceSol), 0.1f); 
    }
}
