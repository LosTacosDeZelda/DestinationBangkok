using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public string typePiegeStr;
   
    private void OnTriggerEnter(Collider trigEnter)
    {

        if (trigEnter.gameObject.tag == "Player")
        {
            trigEnter.gameObject.GetComponent<PlayerController>().JoueurBlesse(typePiegeStr);
        }
       
    }
}
