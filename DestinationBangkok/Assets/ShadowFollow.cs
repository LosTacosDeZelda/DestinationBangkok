using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowFollow : MonoBehaviour
{
    public GameObject player;

    // Update est appelée des dizaines de fois/seconde
    void Update()
    {
        
        transform.position = new Vector3(player.transform.position.x,  player.GetComponent<PlayerController>().playerFeet.point.y + -0.45f, player.transform.position.z);
    }
}
