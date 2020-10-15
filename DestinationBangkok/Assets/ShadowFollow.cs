using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowFollow : MonoBehaviour
{
    public GameObject player;
    SpriteRenderer shadow;

    private void Start()
    {
        shadow = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update est appelée des dizaines de fois/seconde
    void Update()
    {
        
        
        if (player.GetComponent<PlayerController>().playerFeet.transform == null)
        {
            shadow.color = new Color(shadow.color.r, shadow.color.g, shadow.color.b, 0);
        }
        else
        {
            shadow.color = new Color(shadow.color.r, shadow.color.g, shadow.color.b, 1); 

        }

        transform.position = new Vector3(player.transform.position.x,  player.GetComponent<PlayerController>().playerFeet.point.y + 0.01f, player.transform.position.z);
    }
}
