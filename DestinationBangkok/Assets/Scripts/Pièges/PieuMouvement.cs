using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieuMouvement : MonoBehaviour
{
    Rigidbody rbPiege;
    public float distancePieu;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(coroutineMonte());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator coroutineMonte()
    {
        yield return new WaitForSeconds(2.0f);
        rbPiege.velocity = new Vector3(0, distancePieu, 0);
        print("Première coroutine");
        yield return StartCoroutine(coroutineDescend());

    }

    IEnumerator coroutineDescend()
    {
        yield return new WaitForSeconds(2.0f);
        rbPiege.velocity = new Vector3(0, -distancePieu, 0);
        print("Deuxième coroutine");
        yield return StartCoroutine(coroutineMonte());
    }
}
