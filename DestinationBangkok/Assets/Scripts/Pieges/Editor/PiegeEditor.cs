using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

//Auteur : Raphaël Jeudy, et aidé pas mal d'internet ;)
[CustomEditor(typeof(Piege))]
[CanEditMultipleObjects]
public class PiegeEditor : Editor
{
    //Copie des variables dans piège.cs
    SerializedProperty objetMouvementEstParentProp;

    SerializedProperty typePiegeProp;
    SerializedProperty typesDeMouvementProp;

    SerializedProperty axesRotationProp;
    SerializedProperty limiterRotationProp;
    SerializedProperty vitesseRotationUnAxeProp;
    SerializedProperty angleMinProp;
    SerializedProperty angleMaxProp;

    SerializedProperty axesTranslationProp;
    SerializedProperty vitesseTranslationUnAxeProp;
    SerializedProperty posMinProp;
    SerializedProperty posMaxProp;

    float angleMinVar;
    float angleMaxVar;

    float posMinVar;
    float posMaxVar;

    SerializedProperty InstancierÉlémentProp;
    SerializedProperty objetAInstancierProp;
    SerializedProperty vélocitéInstanceProp;
    SerializedProperty délaiInstanceProp;
    SerializedProperty intervalleInstanceProp;
    SerializedProperty délaiDestructionProp;
    SerializedProperty pointDapparitionProp;
    SerializedProperty instanceRotHorizontaleProp;
    SerializedProperty axeAvantProp;
    SerializedProperty rotationInstanceProp;

    private void OnEnable()
    {
        //La pièce du piège qui bouge est-elle parente au collider ?
        objetMouvementEstParentProp = serializedObject.FindProperty("objetMouvementEstParent");

        //Enums
        typePiegeProp = serializedObject.FindProperty("typePiege");
        typesDeMouvementProp = serializedObject.FindProperty("typesDeMouvement");

        //Rotation
        axesRotationProp = serializedObject.FindProperty("axesRotation");
        vitesseRotationUnAxeProp = serializedObject.FindProperty("vitesseRotationUnAxe");
        limiterRotationProp = serializedObject.FindProperty("limiterRotation");
        angleMinProp = serializedObject.FindProperty("angleMin");
        angleMaxProp = serializedObject.FindProperty("angleMax");

        //Translation
        axesTranslationProp = serializedObject.FindProperty("axesTranslation");
        vitesseTranslationUnAxeProp = serializedObject.FindProperty("vitesseTranslationUnAxe");
        posMinProp = serializedObject.FindProperty("posMin");
        posMaxProp = serializedObject.FindProperty("posMax");

        //Instanciation
        InstancierÉlémentProp = serializedObject.FindProperty("InstancierÉlément");
        objetAInstancierProp = serializedObject.FindProperty("objetAInstancier");
        vélocitéInstanceProp = serializedObject.FindProperty("vélocitéInstance");
        délaiInstanceProp = serializedObject.FindProperty("délaiInstance");
        intervalleInstanceProp = serializedObject.FindProperty("intervalleInstance");
        délaiDestructionProp = serializedObject.FindProperty("délaiDestruction");

        pointDapparitionProp = serializedObject.FindProperty("pointDapparition");
        instanceRotHorizontaleProp = serializedObject.FindProperty("instanceRotHorizontale");
        axeAvantProp = serializedObject.FindProperty("axeAvant");
        rotationInstanceProp = serializedObject.FindProperty("rotationInstance");
        


        angleMinVar = angleMinProp.floatValue;
        angleMaxVar = angleMaxProp.floatValue;

        posMinVar = posMinProp.floatValue;
        posMaxVar = posMaxProp.floatValue;

        
        


    }

    bool showRot;
    bool showTrans;
    override public void OnInspectorGUI()
    {

        EditorGUILayout.PropertyField(objetMouvementEstParentProp, new GUIContent("Mouvement sur le parent ?"));
        EditorGUILayout.PropertyField(typePiegeProp, new GUIContent("Type de Piège"));
        EditorGUILayout.PropertyField(typesDeMouvementProp, new GUIContent("Types de Mouvement"));

        switch (typesDeMouvementProp.enumValueIndex)
        {
            case 0:
                showRot = false;
                showTrans = false;
                break;
            case 1:
                showRot = true;
                showTrans = false;
                break;
            case 2:
                showTrans = true;
                showRot = false;
                break;
            case 3:
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

                EditorGUILayout.PropertyField(axesRotationProp,new GUIContent("Axes Rotation"));

                EditorGUILayout.PrefixLabel("Vélocité Rotation");

                /* Début Slider */
                GUIContent label = EditorGUI.BeginProperty(new Rect(0,0,10,10),new GUIContent("Vélocité Rotation"),vitesseRotationUnAxeProp);

                EditorGUI.BeginChangeCheck();
                var valeurSlider = EditorGUILayout.Slider(vitesseRotationUnAxeProp.floatValue, -50, 50);
                // Only assign the value back if it was actually changed by the user.
                // Otherwise a single value will be assigned to all objects when multi-object editing,
                // even when the user didn't touch the control.
                if (EditorGUI.EndChangeCheck())
                {
                    vitesseRotationUnAxeProp.floatValue = valeurSlider;
                }

                EditorGUI.EndProperty();
                /* Fin Slider */

                EditorGUILayout.PropertyField(limiterRotationProp, new GUIContent("Limiter Rotation"));
                EditorGUI.indentLevel--;

                using (var subGroup = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(limiterRotationProp.boolValue)))
                {
                    if (subGroup.visible)
                    {
                        EditorGUI.indentLevel+=2;
                        EditorGUILayout.LabelField("Valeur Min =", Mathf.Round(angleMinProp.floatValue).ToString());
                        EditorGUILayout.LabelField("Valeur Max =", Mathf.Round(angleMaxProp.floatValue).ToString());
                        EditorGUILayout.PrefixLabel("Angle Range");
                        /* Début Min Max */
                        GUIContent labelMinMax = EditorGUI.BeginProperty(new Rect(0, 0, 10, 10), new GUIContent("Limiter Angle"), vitesseRotationUnAxeProp);

                        EditorGUI.BeginChangeCheck();

                        EditorGUILayout.MinMaxSlider(ref angleMinVar, ref angleMaxVar, -180, 180);
                        // Only assign the value back if it was actually changed by the user.
                        // Otherwise a single value will be assigned to all objects when multi-object editing,
                        // even when the user didn't touch the control.
                        if (EditorGUI.EndChangeCheck())
                        {
                            angleMinProp.floatValue = angleMinVar;
                            angleMaxProp.floatValue = angleMaxVar;
                        }

                        EditorGUI.EndProperty();
                        /* Fin */
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

                EditorGUILayout.PropertyField(axesTranslationProp,new GUIContent("Axes Translation"));

                EditorGUILayout.PrefixLabel("Vitesse Translation");
                /* Debut Slider */
                GUIContent labelSlider = EditorGUI.BeginProperty(new Rect(0, 0, 10, 10), new GUIContent("Vitesse Translation"), vitesseTranslationUnAxeProp);

                EditorGUI.BeginChangeCheck();

                var valeurSliderTrans = EditorGUILayout.Slider(vitesseTranslationUnAxeProp.floatValue, -50, 50);
                // Only assign the value back if it was actually changed by the user.
                // Otherwise a single value will be assigned to all objects when multi-object editing,
                // even when the user didn't touch the control.
                if (EditorGUI.EndChangeCheck())
                {
                    vitesseTranslationUnAxeProp.floatValue = valeurSliderTrans;
                }

                EditorGUI.EndProperty();
                /* Fin Slider */

                EditorGUILayout.LabelField("Valeur Min =", Mathf.Round(posMinVar).ToString());
                EditorGUILayout.LabelField("Valeur Max =", Mathf.Round(posMaxVar).ToString());

                /* Debut MinMax */
                GUIContent labelMinMax = EditorGUI.BeginProperty(new Rect(0, 0, 10, 10), new GUIContent("Limiter Translation"), vitesseRotationUnAxeProp);

                EditorGUI.BeginChangeCheck();

                EditorGUILayout.MinMaxSlider(ref posMinVar,ref posMaxVar, -5, 5);
                // Only assign the value back if it was actually changed by the user.
                // Otherwise a single value will be assigned to all objects when multi-object editing,
                // even when the user didn't touch the control.
                if (EditorGUI.EndChangeCheck())
                {
                    posMinProp.floatValue = posMinVar;
                    posMaxProp.floatValue = posMaxVar;
                }

                EditorGUI.EndProperty();
                /* Fin MinMax */
                
                EditorGUI.indentLevel--;
            }
        }

        EditorGUILayout.Space(); 
        EditorGUILayout.LabelField("Module Instance", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(InstancierÉlémentProp, new GUIContent("Instancier Élément"));

        using (var group = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(InstancierÉlémentProp.boolValue)))
        {
            if (group.visible)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(objetAInstancierProp, new GUIContent("Prefab à instancier"));
                EditorGUILayout.PropertyField(pointDapparitionProp, new GUIContent("Point d'apparition de l'instance"));
                EditorGUILayout.PropertyField(vélocitéInstanceProp, new GUIContent("Vélocité de l'instance"));
                EditorGUILayout.PropertyField(instanceRotHorizontaleProp, new GUIContent("Rotation Y de l'instance"));
                EditorGUILayout.PropertyField(délaiInstanceProp, new GUIContent("Délai 1er clone")); //(piege.vélocitéInstance, -20, 20);
                EditorGUILayout.PropertyField(intervalleInstanceProp, new GUIContent("Intervalle entre clones"));
                EditorGUILayout.PropertyField(délaiDestructionProp, new GUIContent("Délai avant destruction"));
                EditorGUILayout.PropertyField(axeAvantProp, new GUIContent("Axe Avant"));
                EditorGUILayout.PropertyField(rotationInstanceProp, new GUIContent("Rotation Instance"));
                EditorGUI.indentLevel--;
            }
        }

        //Sauvegarder les changements aux propriétés
        serializedObject.ApplyModifiedProperties();

    }
}
