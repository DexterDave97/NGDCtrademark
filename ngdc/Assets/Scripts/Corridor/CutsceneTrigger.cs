using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour {

    Animator fadePanel;
    GameObject cutsceneManager;

    private void Start()
    {
        fadePanel = GameObject.FindGameObjectWithTag("FadePanel").GetComponent<Animator>();
        cutsceneManager = GameObject.FindGameObjectWithTag("Cutscene");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            StartCoroutine(TriggerCutscene());
    }
    
    IEnumerator TriggerCutscene()
    {
        fadePanel.SetBool("out", true);
        yield return new WaitForSeconds(1);
        cutsceneManager.SetActive(true);
        Cutscene.cutsceneIndex = 2;
        Cutscene.playCutscene = true;
    }
}
