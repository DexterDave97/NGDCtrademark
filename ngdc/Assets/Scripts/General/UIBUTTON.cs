using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIBUTTON : MonoBehaviour {
    
    GameObject thank;
    public static bool menu = false;

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
        menu = true;
        SceneManager.LoadScene("Credits");
    }

    private void Update()
    {
        if (menu && SceneManager.GetActiveScene().name == "Credits")
            thank.SetActive(false);
        else if (!menu && SceneManager.GetActiveScene().name == "Credits") thank.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "Credits" && menu)
        {
            menu = false;
            SceneManager.LoadScene("MainMenu");
        }

        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "Credits" && !menu)
        {
            Application.Quit();
        }
    }
}
