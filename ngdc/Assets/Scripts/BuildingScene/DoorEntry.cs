using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEntry : MonoBehaviour {

    [SerializeField] GameObject building;
    bool canEnterBuilding = false;
    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            canEnterBuilding = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            canEnterBuilding = false;
    }

    void Update () {
        if (canEnterBuilding == true && Input.GetKeyDown(KeyCode.E))
            player.transform.position = new Vector3(player.transform.position.x + 1.5f, building.transform.position.y + (building.GetComponent<SpriteRenderer>().bounds.extents.y) + player.transform.localScale.y, 0);
    }
}
