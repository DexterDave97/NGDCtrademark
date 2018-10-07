using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChairCutceneTriggerScript : MonoBehaviour {

    bool locked = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        locked = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        locked = false;
    }

    private void Update()
    {
        if (locked && Input.GetKeyDown(KeyCode.E))
            Trig();
    }

    void Trig()
    {
        Cutscene.cutsceneIndex = 9;
        Cutscene.playCutscene = true;
    }
}
