using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : MonoBehaviour
{
    [SerializeField] private float startPos, endPos, speed;
    [SerializeField] private SpriteRenderer rend;
    private Color temp;
    float a;

    void Start()
    {
        a = 0;
        rend = GetComponent<SpriteRenderer>();
        temp = rend.color;
        transform.localPosition = new Vector3(startPos, transform.localPosition.y, transform.localPosition.z);
    }


    void Update()
    {
        Movement();
        if(transform.localPosition.x == endPos)
        {
            Destroy(this.gameObject);
        }
    }

    void Movement()
    {
        a += 3f/(Mathf.Abs((endPos-startPos)) / speed);
        temp.a = Mathf.Clamp(Mathf.Sin(a), 0, 0.5f);
        rend.color = temp;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(endPos, transform.localPosition.y, transform.position.z), speed);
    }
}
