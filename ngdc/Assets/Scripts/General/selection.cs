using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selection : MonoBehaviour {
    [SerializeField] int index = 0;
    [SerializeField] float offset;

    public GameObject sel,gb,highlight;
    public Text tx,first, NextText;
    public string[] dailogues;
    public GameObject[] gbArr;
    public bool skip = false;
    public RollText r;

	// Use this for initialization
	void Start () {
        r = GetComponent<RollText>();
        tx.text = null;
        offset = first.transform.position.y - NextText.transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        moveSel();
        select();

    }
    
    void moveSel()
    {
        if (gb.activeInHierarchy == true)
        {
            highlight.transform.position = gbArr[index].transform.position;
            if (index < 2)
            {
                if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") < 0)
                {
                    index++;
                    sel.transform.position = new Vector3(sel.transform.position.x, sel.transform.position.y - offset, 0);
                    skip = false;
                }
            }

            if (index > 0)
            {
                if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0)
                {
                    index--;
                    sel.transform.position = new Vector3(sel.transform.position.x, sel.transform.position.y + offset, 0);
                    skip = false;
                }
            }
        }
    }

    void select()
    {
        if (gb.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.E) && skip == false)
            {
                StopAllCoroutines();
                StartCoroutine(r.rollText(dailogues[index], tx));
                skip = true;
            }

            else if (Input.GetKeyDown(KeyCode.E) && skip)
            {
                StopAllCoroutines();
                tx.text = dailogues[index];
                skip = false;
            }
        }
    }
}
