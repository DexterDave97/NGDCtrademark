using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeToCorridorTrigger : MonoBehaviour {

    [SerializeField] GameObject player;
    [SerializeField] GameObject cor;
    [SerializeField] Animator elevator;
    [SerializeField] GameObject Black;
    bool locked = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        locked = true;
    }

    private void Update()
    {
        if(locked)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("Black", false);
            GameObject.FindGameObjectWithTag("Footstep").GetComponent<AudioSource>().clip = GameObject.FindGameObjectWithTag("Primary Audio").GetComponent<Sounds>().audioDict["Player"][0];
            Camera.main.transform.position = new Vector3(cor.transform.position.x, cor.transform.position.y, -10);
            Camera.main.orthographicSize = cor.GetComponent<SpriteRenderer>().bounds.extents.x * Screen.height / Screen.width;
            elevator.SetBool("CallElevator", true);
            PlayerController.canmove = false;
            StartCoroutine("Delay");
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.6f);
        if(locked)
            player.transform.position = new Vector3(100, player.transform.position.y, player.transform.position.z);
        PlayerController.canmove = true;
        Black.SetActive(true);
        locked = false;
    }
}
