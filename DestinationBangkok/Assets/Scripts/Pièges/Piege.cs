using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Piege : MonoBehaviour
{
    public GameObject pieu;
    Rigidbody rbPieu;
    public float distancePieu;
    // Start is called before the first frame update
    void Start()
    {
        rbPieu = GetComponent<Rigidbody>();
        StartCoroutine(coroutineMonte());
    }

    // Update is called once per frame
    void Update()
    {
       // transform.Translate(0, Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider infoCollision)
    {
        if (infoCollision.gameObject.tag == "Player")
        {
            print("Manu a touché le pieu, la partie est finie");
            // SceneManager.LoadScene("MenuPrincipal");
        }
    }

    IEnumerator coroutineMonte()
    {
        yield return new WaitForSeconds(2.0f);
        rbPieu.velocity = new Vector3(0, distancePieu, 0);
        print("Première coroutine");
        yield return StartCoroutine(coroutineDescend());

    }

    IEnumerator coroutineDescend()
    {
        yield return new WaitForSeconds(2.0f);
        rbPieu.velocity = new Vector3(0, -distancePieu, 0);
        print("Deuxième coroutine");
        yield return StartCoroutine(coroutineMonte());
    }
}