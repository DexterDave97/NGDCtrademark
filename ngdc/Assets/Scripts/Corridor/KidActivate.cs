using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidActivate : MonoBehaviour {
    public GameObject kid;
	void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Primary Audio").GetComponent<AudioSource>().PlayOneShot(FindObjectOfType<Sounds>().audioDict["Corridor"][4]);
            kid.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
