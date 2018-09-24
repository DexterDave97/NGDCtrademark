using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour {

    [SerializeField] GameObject pop;
    GameObject player;
    float temp;
    bool triggered = false;
    Animator fadePanel;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fadePanel = GameObject.FindGameObjectWithTag("FadePanel").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController.jumpingAvailable = false;
            PlayerController.shouldSuicideBool = true;
        }
    }

    private void Update()
    {
        if (PlayerController.shouldSuicideBool == true)
        {
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            PlayerController.canmove = false;
            player.GetComponent<SpriteRenderer>().flipX = false;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                triggered = true;
                pop.SetActive(false);
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(5.5f, 5), ForceMode2D.Impulse);
                temp = Camera.main.transform.position.y;
            }

            if(triggered)
            {
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + 25, Camera.main.transform.position.z), Time.deltaTime * Time.deltaTime * 5);
                if (Camera.main.transform.position.y >= temp + 25)
                {
                    StartCoroutine(DieBeach());
                }
            }
        }
    }

    IEnumerator DieBeach()
    {
        fadePanel.SetBool("out", true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainMenu");
    }
}
