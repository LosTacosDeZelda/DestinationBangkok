using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
* Script qui gère la gestion de scènes
* Par : Chen Haoyang
*/

public class GestionScene : MonoBehaviour
{
    public void sceneJeu()
  {
    SceneManager.LoadScene("PrototypeNiveau");
    print("On change la scene");
  }

    public void quitterJeu()
  {
    Application.Quit();
    print("Le jeu est fermer, tbk.");
  }
}
