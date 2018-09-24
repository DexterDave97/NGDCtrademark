using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBUTTON : MonoBehaviour {

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
        SceneManager.LoadScene("Credits");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "Credits")
            SceneManager.LoadScene("MainMenu");
    }
}
