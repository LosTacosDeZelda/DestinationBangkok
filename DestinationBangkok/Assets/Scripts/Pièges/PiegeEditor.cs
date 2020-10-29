using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

//Auteur : Par Raphael Jeudy aidé pas mal d'internet ;)

[CustomEditor(typeof(Piege))]
public class PiegeEditor : Editor
{
    float valMin = -180;
    float valMax = 180;
    bool showRot;
    bool showTrans;
    override public void OnInspectorGUI()
    {
        var piege = target as Piege;

        piege.objetMouvementEstParent = EditorGUILayout.Toggle("Mouvement est parent ?", piege.objetMouvementEstParent);

        //EditorGUILayout.ObjectField("Objet en Mouvement", piege.objetMouvement, typeof(GameObject), true) as GameObject;

        piege.typePiege = (Piege.TypePiege) EditorGUILayout.EnumPopup("Type de Pièges", piege.typePiege);
        piege.typesDeMouvement = (Piege.TypeMouvement)EditorGUILayout.EnumPopup("Type de Mouvement", piege.typesDeMouvement);

        switch (piege.typesDeMouvement)
        {
            case Piege.TypeMouvement.Aucun:
                showRot = false;
                showTrans = false;
                break;
            case Piege.TypeMouvement.Rotation:
                showRot = true;
                showTrans = false;
                break;
            case Piege.TypeMouvement.Translation:
                showTrans = true;
                showRot = false;
                break;
            case Piege.TypeMouvement.RotationEtTranslation:
                showRot = true;
                showTrans = true;
                break;
            default:
                break;
        }


        using (var group = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(showRot)))
        {
            if (group.visible)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Rotation", EditorStyles.boldLabel);

                piege.axesRotation = (Piege.Axes)EditorGUILayout.EnumPopup("Axes Rotation", piege.axesRotation);
                EditorGUILayout.PrefixLabel("Vélocité Rotation");
                piege.vitesseRotationUnAxe = EditorGUILayout.Slider(piege.vitesseRotationUnAxe, -50, 50);
                piege.limiterRotation = EditorGUILayout.Toggle("Limiter Rotation", piege.limiterRotation);
                EditorGUI.indentLevel--;

                using (var subGroup = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(piege.limiterRotation)))
                {
                    if (subGroup.visible)
                    {
                        EditorGUI.indentLevel+=2;
                        EditorGUILayout.LabelField("Valeur Min =", Mathf.Round(piege.angleMin).ToString());
                        EditorGUILayout.LabelField("Valeur Max =", Mathf.Round(piege.angleMax).ToString());
                        EditorGUILayout.MinMaxSlider(ref piege.angleMin, ref piege.angleMax, -180, 180);
                        EditorGUI.indentLevel-=2;
                    }
                }
            }
        }

        


        using (var group = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(showTrans)))
        {
            if (group.visible)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Translation", EditorStyles.boldLabel);

                piege.axesTranslation = (Piege.Axes)EditorGUILayout.EnumPopup("Axes Translation", piege.axesTranslation);
                EditorGUILayout.PrefixLabel("Vitesse Translation");
                piege.vitesseTranslationUnAxe = EditorGUILayout.Slider(piege.vitesseTranslationUnAxe, -50, 50);
                EditorGUILayout.LabelField("Valeur Min =", Mathf.Round(piege.posMin).ToString());
                EditorGUILayout.LabelField("Valeur Max =", Mathf.Round(piege.posMax).ToString());
                EditorGUILayout.MinMaxSlider(ref piege.posMin, ref piege.posMax, -5, 5);
                EditorGUI.indentLevel--;
            }
        }

        EditorGUILayout.Space(); 
        EditorGUILayout.LabelField("Module Instance", EditorStyles.boldLabel);
        piege.InstancierÉlément = EditorGUILayout.Toggle("Instancier Élément", piege.InstancierÉlément);

        using (var group = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(piege.InstancierÉlément)))
        {
            if (group.visible)
            {
                EditorGUI.indentLevel++;
                piege.objetAInstancier = EditorGUILayout.ObjectField("Prefab à instancier", piege.objetAInstancier, typeof(GameObject), true) as GameObject;
                piege.vélocitéInstance = EditorGUILayout.Vector3Field("Vélocité de l'instance", piege.vélocitéInstance);
                piege.délaiInstance = EditorGUILayout.FloatField("Délai 1er clone", piege.délaiInstance); //(piege.vélocitéInstance, -20, 20);
                piege.intervalleInstance = EditorGUILayout.FloatField("Intervalle entre clones", piege.intervalleInstance);
                piege.délaiDestruction = EditorGUILayout.IntField("Délai avant destruction", piege.délaiDestruction);
                EditorGUI.indentLevel--;
            }
        }

      
        
    }
}
