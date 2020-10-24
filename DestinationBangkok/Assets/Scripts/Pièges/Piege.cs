using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Piege : MonoBehaviour
{
    public GameObject pieu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision infoCollision)
    {
        if(infoCollision.gameObject.name=="Manu")
        {
            print("Manu a touché le pieu, la partie est finie");
            SceneManager.LoadScene("MenuPrincipal");
        }
    }
}