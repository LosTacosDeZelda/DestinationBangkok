using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GestionStatus : MonoBehaviour
{


  public TextMeshProUGUI texteFeu;
  public TextMeshProUGUI textePerfo;
  public TextMeshProUGUI textePoison;
  public Dictionary<string, int> listeCompteStatus;

  // Start is called before the first frame update
  void Start()
  {
    listeCompteStatus = new Dictionary<string, int>() {
            {"Flamme", 0} ,
            {"Poison", 0} ,
            {"Perforation", 0} ,
        };
  }

  public void MettreAJourTexteFeu()
  {
    texteFeu.text = $"{listeCompteStatus["Flamme"]}";
    textePoison.text = $"{listeCompteStatus["Poison"]}";
    textePerfo.text = $"{listeCompteStatus["Perforation"]}";
  }

}
