using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour {
    public string levelname;
    [SerializeField] bool changable;
    public Animator anim;
    public static string playerPos;

	// Use this for initialization
	void start () {
       
    }
	
	// Update is called once per frame
	void Update () {
        
		if(changable && Input.GetKeyDown(KeyCode.E))
        {
            changable = false;
            StopAllCoroutines();
            StartCoroutine(loadLevel());
        }
	}

    IEnumerator loadLevel()
    {
        playerPos = SceneManager.GetActiveScene().name;
        anim.SetBool("out", true);
        yield return new WaitForSeconds(0.5f);
        PlayerPrefs.SetFloat(playerPos, transform.position.x);
        SceneManager.LoadScene(levelname);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            changable = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            changable = false;
        }
    }
}
