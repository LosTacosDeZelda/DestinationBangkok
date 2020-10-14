using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffichageMenu : MonoBehaviour
{
  //=============================== GAMEOBJECT =================================
  public GameObject TextOptions; // GameObject du text Options page
  public GameObject TextInstructions; // GameObject du text Instructions page

  //====================== Affichage de la page Options ========================
  public void AfficherOptions()
  {
    TextOptions.SetActive(true);
  }

  //====================== Fermer de la page Options ===========================
  public void FermerOptions()
  {
    TextOptions.SetActive(false);
  }

  //===================== Affichage de la page Instrctions =====================
  public void AfficherInstrctions()
  {
    TextInstructions.SetActive(true);
  }

  //===================== Fermer de la page Instrctions ========================
  public void FermerInstrctions()
  {
    TextInstructions.SetActive(false);
  }
}
