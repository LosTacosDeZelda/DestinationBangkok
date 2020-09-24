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

    public GameObject camPivot;
    public GameObject posRaycastCam;

    //Update, mais pour les physiques
    private void FixedUpdate()
    {
       

        //camPivot.transform.RotateAround(camRig.transform.position, Vector3.up,60); 

        camPivot.transform.position = Vector3.Lerp(camPivot.transform.position, cible.transform.position, lerpSpeed);

        transform.LookAt(cibleNormale);


        
        
        //Caméra qui ne traverse pas les murs
        /*if (Physics.Raycast(posRaycastCam.transform.position, transform.forward,out camHit,10))
        {
            // Si touche mur
            if (camHit.collider.gameObject.layer == 9)
            {
                
                cible.transform.localPosition = new Vector3(camHit.point.x , cible.transform.localPosition.y, camHit.point.z);

                //Bouger le Look At aussi
               
              //cibleNormale.transform.position = Vector3.Lerp(cibleNormale.position, GameObject.FindGameObjectWithTag("Player").transform.position, 0.5f);
                
                print("hitting wall");
            }
            else if(camHit.collider.gameObject.layer != 9)
            {
                cibleLookAt = cibleNormale;
               cible.transform.localPosition = new Vector3(cible.transform.localPosition.x, cible.transform.localPosition.y, -distanceCamLoin);
               print("not hitting wall");
            }
            
        }
        //Useless shit (I think)
        else
        {
            
            //cible.transform.localPosition = new Vector3(cible.transform.localPosition.x, cible.transform.localPosition.y, -distanceCamLoin);
           //print("not hitting");
        }*/

       // Debug.DrawLine(posRaycastCam.transform.position, posRaycastCam.transform.position + transform.forward * 10);
    }
}
