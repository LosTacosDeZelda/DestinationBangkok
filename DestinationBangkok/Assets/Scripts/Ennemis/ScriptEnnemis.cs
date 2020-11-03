using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
* Script qui gère les ennemis (Hao, Raph)
*/

public class ScriptEnnemis : MonoBehaviour
{
    //référence au NavMeshAgent de l'ennemie
    NavMeshAgent navAI;
    Animator gardeAnim;

    //référence au GameObject du personnage
    public GameObject personnageAsuivi;

    //référence au GameObject de l'ennemie
    public GameObject ennemiGardien;
    public GameObject yeuxStatue;

    //Boléen pour savoir si l'ennemie est mort
    public bool EnnemiMort;

    //L'ennemi a t-il déjà été réveillé une fois ?
    bool réveillé = false;

    public float distanceLimite;

    Vector3 posInitiale;

    Quaternion rotationInitiale;
    
    float intensitéCouleur = 0;
    bool déjàAppeléRoutine = false;
   

    void Start()
    {
        

        // Raccourcis
        navAI = GetComponent<NavMeshAgent>();
        gardeAnim = GetComponent<Animator>();

        // Au début l'ennemi n'est pas mort
        EnnemiMort = false;

        posInitiale = transform.position;
        rotationInitiale = transform.rotation;

    }

    void Update()
    {
        

        // Si l'ennemi est à une certaine distance, il va commencer à se déplacer
        if (Vector3.Distance(personnageAsuivi.transform.position, transform.position) <= distanceLimite)
        {
            if (déjàAppeléRoutine == false)
            {
                StartCoroutine(RéveilStatue());
            }

            if (réveillé)
            {
                navAI.stoppingDistance = 2;
                navAI.SetDestination(personnageAsuivi.transform.position);

                //Gérer les animations
                gardeAnim.SetBool("bouge", true);
            }
            
            
        }

        else if (Vector3.Distance(personnageAsuivi.transform.position, transform.position) > distanceLimite)
        {
            print("il est parti trop loin!");
            navAI.stoppingDistance = 0;
            navAI.SetDestination(posInitiale);
        }

        if (navAI.remainingDistance < 0.05f)
        {
            gardeAnim.SetBool("bouge", false);

            transform.rotation = rotationInitiale;
        }

        
        
    }

    IEnumerator RéveilStatue()
    {
        if (intensitéCouleur < 1)
        {
            yeuxStatue.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", yeuxStatue.GetComponent<MeshRenderer>().material.color * intensitéCouleur);

            intensitéCouleur += 0.001f;

            //newColor = yeuxStatue.GetComponent<MeshRenderer>().material.color; 

            yield return new WaitForSeconds(0.05f);

            print(intensitéCouleur);

            StartCoroutine(RéveilStatue());
        }
        else
        {
            réveillé = true;
        }
        
    }

}
