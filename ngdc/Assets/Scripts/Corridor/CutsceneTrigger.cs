using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour {

    Animator fadePanel;

    private void Start()
    {
        fadePanel = GameObject.FindGameObjectWithTag("FadePanel").GetComponent<Animator>();
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
        Cutscene.cutsceneIndex = 2;
        Cutscene.playCutscene = true;
        Cutscene.sceneEnd = true;
        Cutscene.nextSceneName = "ToCafe";
    }
}
