using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChairCutceneTriggerScript : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Cutscene.cutsceneIndex = 9;
        SceneManager.LoadScene("PianoScene");
        Cutscene.playCutscene = true;
    }
}
