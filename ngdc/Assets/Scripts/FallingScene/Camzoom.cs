using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camzoom : MonoBehaviour {
    bool lerp;
    [SerializeField] float speed;
	void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.tag=="Player")
        {
            lerp = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (lerp)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 5.2f, speed);
            if (Camera.main.orthographicSize < 5.22f)
            {
                Destroy(this.gameObject);
            }
        }
	}
}
