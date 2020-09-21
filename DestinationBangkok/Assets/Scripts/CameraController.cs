using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    //Cible vers laquelle la caméra va Lerp
    public Transform cible;

    //Cible vers laquelle la caméra va regarder
    public Transform cibleLookAt;
    //Vitesse à laquelle Lerp
    public float lerpSpeed;

    //Update, mais pour les physiques
    private void FixedUpdate()
    {
        //Si cibleLookAt n'est pas assigné, on le trouve et on l'assigne
        //if (cibleLookAt == null)
        //{
         //   cibleLookAt = GameObject.FindGameObjectWithTag("SoccerBall").transform;
       // }

        gameObject.transform.rotation = Quaternion.identity;

        transform.position = Vector3.Lerp(transform.position, cible.transform.position, lerpSpeed);

        transform.LookAt(cibleLookAt);
    }
}
