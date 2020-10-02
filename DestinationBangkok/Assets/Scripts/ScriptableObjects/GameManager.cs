using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class GameManager : ScriptableObject
{
    public TextMeshPro[] textesBoutonsMenu;


    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void OnDestroy()
    {
        
    }

    public void loadScene(int sceneId)
    {
          SceneManager.LoadScene(sceneId);   
    }
}
