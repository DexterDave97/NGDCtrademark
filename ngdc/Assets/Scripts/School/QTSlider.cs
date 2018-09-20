using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTSlider : MonoBehaviour {
    [SerializeField] private GameObject pointer;
    [SerializeField] private float pointerSpeed = 4f;
    [SerializeField] private float pointerLimit = 0.5f;
    [SerializeField] private bool qtActive;
    [SerializeField] private float pointerHitZoneMin;
    [SerializeField] private float pointerHitZoneMax;
    [SerializeField] private Renderer hitZone;
    private float pointerPosAngle;

    // Use this for initialization
    void Start () {
        pointerPosAngle = 0;
        qtActive = false;
        if (hitZone != null)
        {
            pointerHitZoneMin = hitZone.transform.position.x - hitZone.bounds.extents.x - 0.5f;
            pointerHitZoneMax = hitZone.transform.position.x + hitZone.bounds.extents.x + 0.5f;
        }
	}
	
	// Update is called once per frame
	void Update () {
        PointerMove();
        PointerHit();

    }

    void PointerMove()
    {
        pointer.transform.localPosition = new Vector3(Mathf.Sin(pointerPosAngle) * pointerLimit, pointer.transform.localPosition.y, pointer.transform.localPosition.z);
        pointerPosAngle += Time.fixedDeltaTime * pointerSpeed;
    }

    void PointerHit()
    {
        if(qtActive && Input.GetKeyDown(KeyCode.E))
        {
            if (pointer.transform.position.x > pointerHitZoneMin && pointer.transform.position.x < pointerHitZoneMax)
            {
                Debug.Log("hit");
            }
            else
            {
                Debug.Log("miss");
            }
        }
    }
}
