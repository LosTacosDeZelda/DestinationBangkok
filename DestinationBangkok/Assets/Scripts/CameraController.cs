using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject camRig;
    //Cible vers laquelle la caméra va Lerp
    public Transform cible;

    //Cible vers laquelle la caméra va regarder
    Transform cibleLookAt;
    public Transform cibleNormale;
    //Vitesse à laquelle Lerp
    public float lerpSpeed;

    public float distanceCamPres = -5;
    public float distanceCamLoin = -10;
    Vector3 dollyDir;
    public float distance;

    public GameObject camPivot;
    

    private void Awake()
    {
        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }
    //Update, mais pour les physiques
    private void FixedUpdate()
    {
       

        

        camPivot.transform.position = Vector3.Lerp(camPivot.transform.position, cible.transform.position, lerpSpeed);

        transform.LookAt(cibleNormale);

        

        

       

       // Debug.DrawLine(posRaycastCam.transform.position, posRaycastCam.transform.position + transform.forward * 10);
    }

    private void Update()
    {
        /*Vector3 cameraPositionDesiree = transform.parent.TransformPoint(dollyDir * distanceCamLoin);
                RaycastHit hit;

                if (Physics.Linecast(transform.parent.position, cameraPositionDesiree, out hit))
                {
                    distance = Mathf.Clamp(hit.distance, distanceCamPres, distanceCamLoin);
                }
                else
                {
                    distance = distanceCamLoin;
                }

                transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * 10);*/

        Vector3 cameraPositionDesiree = transform.parent.TransformPoint(dollyDir * distanceCamLoin);
        RaycastHit camHit;

        //Caméra qui ne traverse pas les murs
        if (Physics.Linecast(transform.parent.position, cameraPositionDesiree, out camHit))
        {

            distance = Mathf.Clamp(camHit.distance, distanceCamPres, distanceCamLoin);


        }
        else
        {
            distance = distanceCamLoin;

        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * 10);

    }
}
