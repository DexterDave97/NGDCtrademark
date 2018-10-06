using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaughterAtEnd : MonoBehaviour {

    [SerializeField] GameObject kiddo;
    float CamLim;
    bool hit = false, hit2 = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            hit = true;
        }
    }

    private void Update()
    {
        CamLim = Camera.main.transform.position.x + Camera.main.orthographicSize * Screen.width / Screen.height;
        if(hit)
        {
            if(!hit2)
                Camera.main.orthographicSize += (0.5f * Time.deltaTime);
            if (CamLim > 460)
            {
                hit2 = true;
                kiddo.SetActive(true);
            }
        }
    }
}
