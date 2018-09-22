using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CafeCutscene : MonoBehaviour
{
    Animator fadePanel;
    bool trig;

    private void Start()
    {
        trig = false;
        fadePanel = GameObject.FindGameObjectWithTag("FadePanel").GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            trig = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            trig = false;
    }

    IEnumerator TriggerCutscene()
    {
        fadePanel.SetBool("out", true);
        yield return new WaitForSeconds(1);
        Cutscene.cutsceneIndex = 3;
        Cutscene.playCutscene = true;
    }

    private void Update()
    {
        if (trig && Input.GetKeyDown(KeyCode.E))
        {
            trig = false;
            StartCoroutine(TriggerCutscene());
        }
    }
}

