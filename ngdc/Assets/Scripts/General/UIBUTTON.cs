using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIBUTTON : MonoBehaviour {
    
    GameObject thank;

    private void Awake()
    {
        if(SceneManager.GetActiveScene().name == "Credits")
            thank = GameObject.FindGameObjectWithTag("Thanks");
    }

    public void PlayMe()
    {
        SceneManager.LoadScene("House");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ToCredit()
    {
        SceneManager.LoadScene("Credits 2");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "Credits 2")
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (Input.GetKeyDown(KeyCode.Escape) && (SceneManager.GetActiveScene().name == "Credits" || SceneManager.GetActiveScene().name == "Credits 1"))
        {
            Application.Quit();
        }
    }
}
