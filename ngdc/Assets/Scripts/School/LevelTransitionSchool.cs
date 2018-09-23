using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitionSchool : MonoBehaviour {
    public string levelname;
    Animator anim;

    private void Start()
    {
        anim = GameObject.FindGameObjectWithTag("FadePanel").GetComponent < Animator > ();
    }

    IEnumerator loadLevel()
    {
        anim.SetBool("out", true);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(levelname);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            StopAllCoroutines();
            StartCoroutine(loadLevel());
        }
    }
}