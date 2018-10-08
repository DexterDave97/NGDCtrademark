using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBUTTON : MonoBehaviour {

    public static bool menu = false;

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
