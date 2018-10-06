using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour {

    GameObject player;
    [SerializeField] Transform res;
    
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneManager.LoadScene("School");
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            player.transform.position = res.transform.position + new Vector3(0, (res.transform.localScale.y / 2) + player.GetComponent<SpriteRenderer>().bounds.extents.y, 0);
            PlayerController.Dir = 0;
            Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        }
    }
}
