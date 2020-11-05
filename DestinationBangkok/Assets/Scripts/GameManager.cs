using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Ici, on gère la logique générale du jeu : état du jeu (jeu en cours, fini)
//le chargement des scènes, les changements de caméra, les cinématiques et le UI
public class GameManager : MonoBehaviour
{
    public bool joueurEstMort;
    public GameObject joueur;

    [Header("Caméras")]
    public GameObject camPrincipale;
    public GameObject cameraMort;

    public GameObject postProcessing;
    public GameObject panneauNoir;

  
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update est appelée plusieurs fois par secondes
    void Update()
    {
        joueurEstMort = joueur.GetComponent<PlayerController>().estMort;

        if (joueurEstMort)
        {
           StartCoroutine(SequenceDeMort());
        }
    }

    IEnumerator SequenceDeMort()
    {
        //Changer de caméra
        camPrincipale.SetActive(false);
        cameraMort.SetActive(true);

        //Désactiver les script qui fait que la caméra suit le joueur
        cameraMort.transform.parent.GetComponent<CameraFollow>().enabled = false;

        cameraMort.GetComponent<Animator>().SetBool("travellingAvant", true);



        //Fondu au noir, on recharge la scène

        yield return new WaitForSeconds(7f);
        panneauNoir.GetComponent<Animator>().SetTrigger("versNoir");

        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
