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
    public Vector3 vélocitéInstance;
    public float délaiInstance;
    public float intervalleInstance;
    public int délaiDestruction;

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

        print(rbPiege.gameObject);
        rbPiege.useGravity = false;
        positionDépart = rbPiege.transform.position;

        //Appeler la coroutine d'instanciation si l'utilisateur a coché la case
        if (InstancierÉlément)
        {
            StartCoroutine(Instanciation());
        }
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public void Reset()
    {
        ParticleSystem particle = GetComponent<ParticleSystem>();
        Collider collider = GetComponent<Collider>();
        Rigidbody rb = GetComponent<Rigidbody>();

        if (particle == null && collider == null || rb == null)
        {
            if (UnityEditor.EditorUtility.DisplayDialog("Choisis un type de piège", "Il te manque des composants pour créer ton piège ! Choisis une des deux options de création :", " À base d'un Particle System", " À base d'un Collider standard"))
            {
                if (gameObject.transform.parent == null)
                {
                    gameObject.AddComponent<ParticleSystem>();

                    if (UnityEditor.EditorUtility.DisplayDialog("Attention !", "N'oublies pas que le système de particules doit être enfant d'un objet.", "Ben crée le parent, sti","Ça vaaa, arrête de me gosser bro"))
                    {
                        GameObject objetParent = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        objetParent.AddComponent<Rigidbody>();
                        objetParent.transform.position = transform.position;
                        gameObject.transform.parent = objetParent.transform;
                        objetMouvementEstParent = true;
                    }
                    else
                    {
                        print("Fuck YOUU");
                    }
                }
                else
                {
                    gameObject.transform.parent.gameObject.AddComponent<Rigidbody>();
                    gameObject.transform.parent.gameObject.AddComponent<BoxCollider>();
                    gameObject.AddComponent<ParticleSystem>();
                }
                

                
            }
            else
            {
                gameObject.AddComponent<Rigidbody>();
                gameObject.AddComponent<BoxCollider>();
            }
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

        GameObject instance = Instantiate(objetAInstancier, rbPiege.position, Quaternion.identity);
        instance.GetComponent<Rigidbody>().velocity = vélocitéInstance;

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