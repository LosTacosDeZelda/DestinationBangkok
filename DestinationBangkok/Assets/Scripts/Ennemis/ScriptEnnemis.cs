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
  public Material corpsGarde;

  public float valeurTransition;
  public float t = 0f;
  public float t2 = 0f;

  public float tempsRéveil;
  public float compteurRéveil;
  public float tempsEndort;
  public float compteurEndort;



  //référence au GameObject du joueur
  public GameObject personnageAsuivi;

  //référence au GameObject de l'ennemie
  public GameObject ennemiGardien;
  public GameObject yeuxStatue;

  //L'ennemi a t-il déjà été réveillé une fois ?
  bool réveillé = false;

  public float distanceLimite;

  Vector3 posInitiale;
  Quaternion rotationInitiale;

  float intensitéCouleur = 0;
  bool déjàAppeléRoutine = false;
  bool déjàAppeléRoutineEndort = false;

  private IEnumerator coroutineRéveil;
  private IEnumerator coroutineSommeil;


    void Start()
  {


    // Raccourcis
    navAI = GetComponent<NavMeshAgent>();
    gardeAnim = GetComponent<Animator>();
    corpsGarde = gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material;

    // On retient la position et la rotation initiale du garde
    posInitiale = transform.position;
    rotationInitiale = transform.rotation;

    //Références aux 2 coroutines

    coroutineRéveil = RéveilStatue();
    coroutineSommeil = EndortStatue();

        
            

  }
    bool partirCompteur = false;
    bool partirCompteur2 = false;
  void Update()
  {
        if (partirCompteur)
        {
            compteurRéveil += Time.deltaTime;
        }

        if (partirCompteur2)
        {
            compteurEndort += Time.deltaTime;
        }

    // Si le joueur est proche de l'ennemi (à une certaine distance), celui-ci va commencer à se déplacer vers le joueur
    if (Vector3.Distance(personnageAsuivi.transform.position, transform.position) <= distanceLimite)
    {
      
      // Si le joueur est proche, mais que le garde n'est pas réveillé, entamer la séquence de réveil
      if (réveillé == false)
      {
         
         if (déjàAppeléRoutine == false)
         {
            déjàAppeléRoutine = true;
            partirCompteur = true;
            StartCoroutine(RéveilStatue());
         }
         
        
      }
      else // Dès que le garde est réveillé, poursuivre le joueur
      {
         
         navAI.stoppingDistance = 2;
         navAI.SetDestination(personnageAsuivi.transform.position);

         //Gérer les animations de marche
         gardeAnim.SetBool("bouge", true);
      }


    } //Si le joueur est trop loin, l'ennemi revient vers sa position initiale
    else if (Vector3.Distance(personnageAsuivi.transform.position, transform.position) > distanceLimite)
    {
      navAI.stoppingDistance = 0;
      navAI.SetDestination(posInitiale);

      //Si le garde est rendu à sa position de départ et qu'il est réveillé, entamer la  séquence d'endormissement
      if (navAI.remainingDistance < 0.05f && réveillé)
      {
                gardeAnim.SetBool("bouge", false);
                transform.rotation = rotationInitiale;

                //Partir le compteur
                
                if (déjàAppeléRoutineEndort == false)
                {
                    déjàAppeléRoutineEndort = true;
                    partirCompteur2 = true;
                    StartCoroutine(EndortStatue());
                    
                }

      }

            
    }


  }

  IEnumerator RéveilStatue()
  {
   

    if (compteurRéveil < tempsRéveil)
    {
            
       if (intensitéCouleur < 1)
       {
            yeuxStatue.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", yeuxStatue.GetComponent<MeshRenderer>().material.color * intensitéCouleur);

            intensitéCouleur += (0.06f / tempsRéveil);
       }

       //(Mathf.Pow(tempsRéveil, 2f)
      corpsGarde.SetFloat("Vector1_63B5137F", valeurTransition);

      valeurTransition = Mathf.Lerp(0, 1, t);

      t += (0.06f / tempsRéveil);
            
            
      yield return new WaitForSeconds(0.05f);
            
      print("reveilStatue running");

      StartCoroutine(RéveilStatue());

    }
    else
    {

       réveillé = true;
       compteurRéveil = 0;
            t = 0;
       partirCompteur = false;
       déjàAppeléRoutine = false;
       StopCoroutine(coroutineRéveil);

    }




  }

  IEnumerator EndortStatue()
  {
        if (compteurEndort < tempsEndort)
        {

            if (intensitéCouleur > 0)
            {
                yeuxStatue.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", yeuxStatue.GetComponent<MeshRenderer>().material.color * intensitéCouleur);

                intensitéCouleur -= (0.06f / tempsEndort);
            }


            corpsGarde.SetFloat("Vector1_63B5137F", valeurTransition);

            valeurTransition = Mathf.Lerp(1, 0, t2);

            t2 += (0.06f / tempsEndort);


            yield return new WaitForSeconds(0.05f);

            print("endortStatue running");

            StartCoroutine(EndortStatue());
        }
        else
        {
            réveillé = false;
            compteurEndort = 0;
            t2 = 0;
            déjàAppeléRoutineEndort = false;
            partirCompteur2 = false;
            StopCoroutine(coroutineSommeil);
        }
        
  }

}
