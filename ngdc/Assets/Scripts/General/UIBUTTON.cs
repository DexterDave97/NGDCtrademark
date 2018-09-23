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
}
