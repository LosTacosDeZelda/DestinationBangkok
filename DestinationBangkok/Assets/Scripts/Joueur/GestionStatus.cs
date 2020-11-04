using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestionStatus : MonoBehaviour
{


  public Text texteFeu;
  public Text textePerfo;
  public Text textePoison;
  public Dictionary<string, int> listeCompteStatus;

  // Start is called before the first frame update
  void Start()
  {
    listeCompteStatus = new Dictionary<string, int>() {
            {"Flamme", 0} ,
            {"Perforation", 0} ,
            {"Poison", 0} ,
        };
  }

  public void MettreAJourTexteFeu()
  {
    texteFeu.text = $"{listeCompteStatus["Flamme"]} / 3 Feu";
    textePerfo.text = $"{listeCompteStatus["Perforation"]} / 3 Perforation";
    textePoison.text = $"{listeCompteStatus["Poison"]} / 3 Poison";
  }

}
