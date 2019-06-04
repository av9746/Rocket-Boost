using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugManager : MonoBehaviour
{
    [SerializeField]private float levelLoad = 1f;

    void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            Invoke("LoadNextLevel", levelLoad);
        }

        if (Input.GetKey((KeyCode.C)))
        {
            
        }
    }

    private void LoadNextLevel()
    {
        var currentscene = SceneManager.GetActiveScene().buildIndex;
        
        if (currentscene == 4)
        {
            currentscene = -1;
        }
        SceneManager.LoadScene(currentscene + 1);
    }
}
