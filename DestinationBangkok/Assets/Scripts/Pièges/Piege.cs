using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Auteurs : François et Raphaël

public class Piege : MonoBehaviour
{
    public GameObject objetMouvement;
    Rigidbody rbPiege;
    public enum TypePiege
    {
        Flamme,
        Perforation,
        Poison,
    }

    public TypePiege typePiege;

    public enum TypeMouvement
    {
        Aucun,
        Rotation,
        Translation,
        RotationEtTranslation,
    }

    public TypeMouvement typesDeMouvement;

    public enum AxeRot
    {
        X,
        Y,
        Z,
    }

    public AxeRot axeRotation;

    
    
    public float velociteUnAxe;
    public float angleMax;
    public float angleMin;

    float rotUnAxe;
    float angleMaxQuat;
    float angleMinQuat;

    // Start is called before the first frame update
    void Start()
    {
        rbPiege = objetMouvement.GetComponent<Rigidbody>();

        switch (typesDeMouvement)
        {
            case TypeMouvement.Rotation:

                StartCoroutine(Rotation());

                break;

            case TypeMouvement.Translation:

                StartCoroutine(Translation());

                break;

            case TypeMouvement.RotationEtTranslation:

                StartCoroutine(Translation());
                StartCoroutine(Rotation());

                break;

            default:
                break;
        }

        


    }

    
    private void FixedUpdate()
    {

        StartCoroutine(Rotation());

        switch (axeRotation)
        {
            case AxeRot.X:
                rotUnAxe = rbPiege.rotation.x;
                angleMaxQuat = Quaternion.Euler(new Vector3(angleMax,0,0)).x;
                angleMinQuat = Quaternion.Euler(new Vector3(angleMin, 0, 0)).x;
                vitesseRotation = new Vector3(velociteUnAxe, 0, 0);

                break;
            case AxeRot.Y:
                rotUnAxe = rbPiege.rotation.y;
                angleMaxQuat = Quaternion.Euler(new Vector3(0, angleMax, 0)).y;
                angleMinQuat = Quaternion.Euler(new Vector3(0, angleMin, 0)).y;
                vitesseRotation = new Vector3(0, velociteUnAxe, 0);
                break;
            case AxeRot.Z:
                rotUnAxe = rbPiege.rotation.z;
                angleMaxQuat = Quaternion.Euler(new Vector3(0, 0, angleMax)).z;
                angleMinQuat = Quaternion.Euler(new Vector3(0, 0, angleMin)).z;
                vitesseRotation = new Vector3(0, 0, velociteUnAxe);
                break;
            default:
                break;
        }

    }



    private void OnCollisionEnter(Collision infoCollision)
    {
        if (infoCollision.gameObject.tag == "Player")
        {
            infoCollision.gameObject.GetComponent<PlayerController>().JoueurBlesse(typePiege.ToString());
            print("Manu a touché un piège de type " + typePiege.ToString() + ", la partie est finie");

        }
    }

    private void OnTriggerEnter(Collider infoCollision)
    {
        if (infoCollision.gameObject.tag == "Player")
        {
            infoCollision.gameObject.GetComponent<PlayerController>().JoueurBlesse(typePiege.ToString());
            print("Manu a touché un piège de type " + typePiege.ToString() + ", la partie est finie");
            
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().JoueurBlesse(typePiege.ToString());
            print("Manu a touché un piège de type " + typePiege.ToString() + ", la partie est finie");

        }
    }

    IEnumerator Translation()
    {
        //rbPiege.MovePosition()
        yield return new WaitForSeconds(2);
    }

    Vector3 vitesseRotation;
    
    bool clockwise = false;
    
    
    IEnumerator Rotation()
    {
        
        if (clockwise == false)
        {
            
            yield return new WaitForSeconds(0f);
            rbPiege.angularVelocity = vitesseRotation;

            if (rotUnAxe > angleMaxQuat)
            {
                
                clockwise = true;
            }
        }
        
        if (clockwise == true)
        {
            
            yield return new WaitForSeconds(0f);
            rbPiege.angularVelocity = -vitesseRotation;

            if (rotUnAxe < angleMinQuat)
            {
                clockwise = false;
            }

        }
        
    }

}