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
        if (Input.GetKeyDown(KeyCode.E) && locked)
            Trig();
    }

    void Trig()
    {
        Cutscene.cutsceneIndex = 9;
        GameObject.FindGameObjectWithTag("FadePanel").GetComponent<Animator>().SetBool("out", true);
        StartCoroutine("Hello");
    }

    IEnumerator Hello()
    {
        yield return new WaitForSeconds(0.5f);
        Cutscene.playCutscene = true;
    }
}