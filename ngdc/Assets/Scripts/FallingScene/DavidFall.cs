using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DavidFall : MonoBehaviour {

    public float var;
    Rigidbody2D play;
    public bool trig;

    private void Start()
    {
        play = GetComponent<Rigidbody2D>();
        if (trig)
            play.velocity = Vector3.down * var;
        else
        {
            play.velocity = Vector3.left * var;
            StartCoroutine(Hello());
        }
    }

    IEnumerator Hello()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
