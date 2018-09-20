using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidActivate : MonoBehaviour {
    public GameObject kid;
	void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            kid.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
