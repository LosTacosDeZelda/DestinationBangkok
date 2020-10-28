using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Auteurs : François et Raphaël Jeudy

public class Piege : MonoBehaviour
{
    
    public GameObject objetMouvement;
    Rigidbody rbPiege;
    Vector3 positionDépart;
    
    public enum TypePiege
    {
        Flamme,
        Perforation,
        Poison,
        Glacé,
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

    public enum Axes
    {
        X,
        Y,
        Z,
    }

    public Axes axesRotation;
 
    public bool limiterRotation;
    public float vitesseRotationUnAxe;
    float rotAxeCourant;
    public float angleMax;
    public float angleMin;

    float angleMaxQuat;
    float angleMinQuat;


    public Axes axesTranslation;

    public float vitesseTranslationUnAxe;
    float posAxeCourant;
    public float posMax;
    public float posMin;

    public bool InstancierÉlément;
    public GameObject objetAInstancier;

    // Start est appelée avant la première image 
    void Start()
    {
        rbPiege = objetMouvement.GetComponent<Rigidbody>();
        positionDépart = objetMouvement.transform.position;
    }

    bool appelleFonction = true;
    private void FixedUpdate()
    {
        

            //Changer le type de mouvement du piège
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

        //Changer l'axe de rotation du piège
        switch (axesRotation)
        {
            case Axes.X:
                rotAxeCourant = rbPiege.rotation.x;
                angleMaxQuat = Quaternion.Euler(new Vector3(angleMax,0,0)).x;
                angleMinQuat = Quaternion.Euler(new Vector3(angleMin, 0, 0)).x;
                vitesseRotation = new Vector3(vitesseRotationUnAxe, 0, 0);

                break;
            case Axes.Y:
                rotAxeCourant = rbPiege.rotation.y;
                angleMaxQuat = Quaternion.Euler(new Vector3(0, angleMax, 0)).y;
                angleMinQuat = Quaternion.Euler(new Vector3(0, angleMin, 0)).y;
                vitesseRotation = new Vector3(0, vitesseRotationUnAxe, 0);
                break;
            case Axes.Z:
                rotAxeCourant = rbPiege.rotation.z;
                angleMaxQuat = Quaternion.Euler(new Vector3(0, 0, angleMax)).z;
                angleMinQuat = Quaternion.Euler(new Vector3(0, 0, angleMin)).z;
                vitesseRotation = new Vector3(0, 0, vitesseRotationUnAxe);
                break;
            default:
                break;
        }

        switch (axesTranslation)
        {
            // pos dep = 50, pos courante = 70
            case Axes.X:
                posAxeCourant = rbPiege.transform.position.x - positionDépart.x;
                vélocitésTranslation = new Vector3(vitesseTranslationUnAxe, 0, 0);
                break;
            case Axes.Y:
                posAxeCourant = rbPiege.transform.position.y - positionDépart.y;
                vélocitésTranslation = new Vector3(0, vitesseTranslationUnAxe, 0);
                break;
            case Axes.Z:
                posAxeCourant = rbPiege.transform.position.z - positionDépart.z;
                vélocitésTranslation = new Vector3(0, 0, vitesseTranslationUnAxe);
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
    Vector3 vélocitésTranslation;
    bool sensNormal = true;
    IEnumerator Translation()
    {
        

        // print(posAxeCourant);
        
        if (sensNormal == true)
        {
            
            yield return new WaitForSeconds(0f);
            
            rbPiege.velocity = vélocitésTranslation;

            if (posAxeCourant > posMax)
            {

                sensNormal = false;
            }
        }

        if (sensNormal == false)
        {
            
            yield return new WaitForSeconds(0f);
            
            rbPiege.velocity = -vélocitésTranslation;

            if (posAxeCourant < posMin)
            {
                sensNormal = true;
            }

        }
        
        
    }

    Vector3 vitesseRotation;
    bool clockwise = false;
    
    IEnumerator Rotation()
    {
        
        if (clockwise == false)
        {
            
            yield return new WaitForSeconds(0f);
            rbPiege.angularVelocity = vitesseRotation;

            if (rotAxeCourant > angleMaxQuat && limiterRotation)
            {
                
                clockwise = true;
            }
        }
        
        if (clockwise == true)
        {
            yield return new WaitForSeconds(0f);
            rbPiege.angularVelocity = -vitesseRotation;

            if (rotAxeCourant < angleMinQuat && limiterRotation)
            {
                clockwise = false;
            }

        }
        
    }

}