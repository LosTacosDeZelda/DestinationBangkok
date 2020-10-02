using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject camRig;
    //Cible vers laquelle la caméra va Lerp
    public Transform cible;

    //Cible vers laquelle la caméra va regarder
   
    public Transform cibleLookAt;
    //Vitesse à laquelle Lerp
    public float lerpSpeed;

    public float distanceCamPres = 5;
    public float distanceCamLoin = 10;
    Vector3 dollyDir;
    public float distance;


    private void Awake()
    {
        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }
    //Update, mais pour les physiques
    private void FixedUpdate()
    {
       

        

        transform.position = Vector3.Lerp(transform.position, cible.transform.position, lerpSpeed);

        transform.LookAt(cibleLookAt);
    }

    private void Update()
    {

        Vector3 cameraPositionDesiree = transform.TransformPoint(dollyDir * distanceCamLoin);
        RaycastHit camHit;

        //Caméra qui ne traverse pas les murs
        if (Physics.Linecast(transform.position, transform.position + transform.forward * 7, out camHit))
        {

            distance = Mathf.Clamp(camHit.distance, distanceCamPres, distanceCamLoin);


        }
        else
        {
            distance = distanceCamLoin;

        }

        Debug.DrawLine(transform.position,transform.position + transform.forward * 7, Color.cyan);

        //transform.position = Vector3.Lerp(transform.position, dollyDir * distance, Time.deltaTime * 10);

    }
}
