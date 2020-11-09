using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifyTrap : MonoBehaviour
{
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public void Reset()
    {
        ParticleSystem particle = GetComponent<ParticleSystem>();
        Collider collider = GetComponent<Collider>();
        Rigidbody rb = GetComponent<Rigidbody>();

        if (particle == null && collider == null || rb == null)
        {
            if (UnityEditor.EditorUtility.DisplayDialog("Choisis un type de piège", "Il te manque des composants pour créer ton piège ! Choisis une des deux options de création :", " À base d'un Particle System", " À base d'un Collider standard"))
            {
                if (gameObject.transform.parent == null)
                {
                    gameObject.AddComponent<ParticleSystem>();

                    if (UnityEditor.EditorUtility.DisplayDialog("Attention !", "N'oublies pas que le système de particules doit être enfant d'un objet.", "Ben crée le parent, sti", "Ça vaaa, arrête de me gosser bro"))
                    {
                        GameObject objetParent = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        objetParent.AddComponent<Rigidbody>();
                        objetParent.transform.position = transform.position;
                        gameObject.transform.parent = objetParent.transform;
                        //objetMouvementEstParent = true;
                    }
                    else
                    {
                        print("Je taime pas yo");
                    }
                }
                else
                {
                    gameObject.transform.parent.gameObject.AddComponent<Rigidbody>();
                    gameObject.transform.parent.gameObject.AddComponent<BoxCollider>();
                    gameObject.AddComponent<ParticleSystem>();
                }



            }
            else
            {
                gameObject.AddComponent<Rigidbody>();
                gameObject.AddComponent<BoxCollider>();
            }
        }
    }
}
