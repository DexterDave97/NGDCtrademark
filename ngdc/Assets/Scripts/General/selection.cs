using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selection : MonoBehaviour {
    [SerializeField] int index = 0;
    [SerializeField] float offset;
    public GameObject sel,gb,highlight;
    public Text tx;
    public string[] dailogues;
    public GameObject[] gbArr;
    public RollText rt;
    public bool skip = false;

	// Use this for initialization
	void Start () {
        tx.text = null;
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
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    index++;
                    sel.transform.position = new Vector3(sel.transform.position.x, sel.transform.position.y - offset, 0);
                    skip = false;
                }
            }

            if (index > 0)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
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
            if (Input.GetKeyDown(KeyCode.Return) && skip == false)
            {
                StopAllCoroutines();
                StartCoroutine(rt.rollText(dailogues[index], tx));
                skip = true;
            }

            else if (Input.GetKeyDown(KeyCode.Return) && skip)
            {
                StopAllCoroutines();
                tx.text = dailogues[index];
                skip = false;
            }
        }
    }
}
