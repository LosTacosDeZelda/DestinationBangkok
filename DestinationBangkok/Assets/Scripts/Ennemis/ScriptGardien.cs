using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
* Script qui gère l'ennemi Gardien
*/

public class ScriptGardien : MonoBehaviour
{
    //référence au NavMeshAgent de l'ennemie
    NavMeshAgent navAI;

    //référence au GameObject du personnage
    public GameObject personnageAsuivi;

    //référence au GameObject de l'ennemie
    public GameObject ennemiGardien;

    //Boléen pour savoir si l'ennemie est mort
    public bool EnnemiMort;

    public Vector3 positionInitialEnnemi;

    public float distanceLimite;

    void Start()
    {
    // Raccourcis
    navAI = GetComponent<NavMeshAgent>();

    // Au début l'ennemi n'est pas mort
    EnnemiMort = false;

    positionInitialEnnemi = ennemiGardien.transform.position;
    }

    void Update()
    {
    // Si l'ennemi est à une certaine distance, il va commencer à se déplacer
    if (Vector3.Distance(personnageAsuivi.transform.position, transform.position) <= distanceLimite && EnnemiMort == false)
    {
      navAI.enabled = true;
      navAI.SetDestination(personnageAsuivi.transform.position);
    }

    else if (Vector3.Distance(personnageAsuivi.transform.position, transform.position) > distanceLimite && EnnemiMort == false)
    {
      print("il est parti trop loin!");
      navAI.SetDestination(positionInitialEnnemi);
    }
  }
}
