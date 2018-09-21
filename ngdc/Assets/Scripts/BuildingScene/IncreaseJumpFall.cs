using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseJumpFall : MonoBehaviour
{
    GameObject cutsceneManager;
    Animator fadePanel;

    private void Awake()
    {
        cutsceneManager = GameObject.FindGameObjectWithTag("Cutscene");
        fadePanel = GameObject.FindGameObjectWithTag("FadePanel").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(TriggerCutscene());
        }
    }
    
    IEnumerator TriggerCutscene()
    {
        fadePanel.SetBool("out", true);
        yield return new WaitForSeconds(1);
        cutsceneManager.SetActive(true);
        Cutscene.cutsceneIndex = 5;
        Cutscene.playCutscene = true;
        Destroy(this.gameObject);
    }
}
