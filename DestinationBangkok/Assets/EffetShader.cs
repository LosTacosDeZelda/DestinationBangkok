using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetShader : MonoBehaviour
{
    public Material materiauGarde;
    public float valeurTransition;
    public float maxTransition;

    private void Start()
    {
        materiauGarde.SetFloat("Vector1_63B5137F", valeurTransition);
    }

    private void Update()
    {
        
        
    }
}
