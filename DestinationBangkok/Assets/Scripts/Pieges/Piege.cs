using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Auteur : Raphaël Jeudy
//Script Modulaire qui permet de créer plusieurs types de pièges

public class Piege : MonoBehaviour
{
    
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

    //Variables Principales
    public bool objetMouvementEstParent;
    Rigidbody rbPiege;
    Vector3 positionDépart;

    //Rotation
    public Axes axesRotation;
 
    public bool limiterRotation;
    public float vitesseRotationUnAxe;
    float rotAxeCourant;
    public float angleMax;
    public float angleMin;

    float angleMaxQuat;
    float angleMinQuat;

    //Translation
    public Axes axesTranslation;

    public float vitesseTranslationUnAxe;
    float posAxeCourant;
    public float posMax;
    public float posMin;

    //Module instance
    public bool InstancierÉlément;

    public GameObject objetAInstancier;
    public float vélocitéInstance;
    public float délaiInstance;
    public float intervalleInstance;
    public int délaiDestruction;

    public GameObject pointDapparition;
    public float instanceRotHorizontale;
    public Axes axeAvant;
    int axeChoisi;
    Vector3 axeTransform;
    public Vector3 rotationInstance;

    

    // Start est appelée dès que le jeu roule
    void Start()
    {
        if (objetMouvementEstParent)
        {
            rbPiege = GetComponentInParent<Rigidbody>();
        }
        else
        {
            rbPiege = GetComponent<Rigidbody>();
        }

        rbPiege.useGravity = false;
        positionDépart = rbPiege.transform.position;

        
        //axesTransform[0] = transform.forward;
        //axesTransform[1] = transform.right;
        //axesTransform[2] = transform.up;


        //Appeler la coroutine d'instanciation si l'utilisateur a coché la case
        if (InstancierÉlément)
        {
            StartCoroutine(Instanciation());
        }
    }

    

    float tempsDepuisInstance;
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

        //Changer la translation du piege
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

        switch (axeAvant)
        {
            case Axes.X:
                axeTransform = transform.right;
                break;
            case Axes.Y:
                axeTransform = transform.up;
                break;
            case Axes.Z:
                axeTransform = transform.forward;
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
            

        }
    }

    private void OnTriggerEnter(Collider infoCollision)
    {
        if (infoCollision.gameObject.tag == "Player")
        {
            infoCollision.gameObject.GetComponent<PlayerController>().JoueurBlesse(typePiege.ToString());
            
            
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().JoueurBlesse(typePiege.ToString());
            

        }
    }
    Vector3 vélocitésTranslation;
    bool sensNormal = true;

    /**
     * Gère la translation aller-retour du piège sur un axe, relative à la position du piège
     */
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
    bool sensHoraire = false;

    /**
     * Gère la rotation sans-arrêt ou aller-retour du piège sur un axe
     * 
     */
    IEnumerator Rotation()
    {
        
        if (sensHoraire == false)
        {
            
            yield return new WaitForSeconds(0f);

            rbPiege.transform.Rotate(vitesseRotation);
            

            if (rotAxeCourant > angleMaxQuat && limiterRotation)
            {
                
                sensHoraire = true;
            }
        }
        
        if (sensHoraire == true)
        {
            yield return new WaitForSeconds(0f);
            rbPiege.transform.Rotate(-vitesseRotation);

            if (rotAxeCourant < angleMinQuat && limiterRotation)
            {
                sensHoraire = false;
            }

        }
        
    }

    bool premierAppelInstanciation = true;
    IEnumerator Instanciation()
    {
        //Délai initial
        if (premierAppelInstanciation)
        {
            premierAppelInstanciation = false;
            yield return new WaitForSeconds(délaiInstance);
        }

        GameObject instance = Instantiate(objetAInstancier, pointDapparition.transform.position, transform.rotation);
        instance.GetComponent<Projectile>().typePiegeStr = typePiege.ToString();
        //instance.transform.rotation = Quaternion.Euler(rotationInstance);
        instance.GetComponent<Rigidbody>().velocity = axeTransform * vélocitéInstance;

        //Appeler la fonction pour détruire l'instance dans un certain temps
        DétruireObjet(instance);

        //Attendre un peu avant d'appeler la fonction une nouvelle fois
        yield return new WaitForSeconds(intervalleInstance);

        StartCoroutine(Instanciation());
    }

    void DétruireObjet(GameObject àDétruire)
    {
        Destroy(àDétruire, délaiDestruction);
    }

}