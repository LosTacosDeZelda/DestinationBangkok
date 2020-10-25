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
        StartCoroutine(coroutineMonte());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, Time.deltaTime, 0);
    }

    IEnumerator coroutineMonte()
    {
        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(coroutineDescend());

    }

    IEnumerator coroutineDescend()
    {
        yield return new WaitForSeconds(1.0f);
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