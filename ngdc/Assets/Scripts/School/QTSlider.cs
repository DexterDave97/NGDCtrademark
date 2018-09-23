using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTSlider : MonoBehaviour {
    [SerializeField] private GameObject pointer;
    [SerializeField] private float pointerSpeed = 4f;
    [SerializeField] private float pointerLimit = 0.5f;
    private float pointerHitZoneMin;
    private float pointerHitZoneMax;
    [SerializeField] private Image hitZone, qTBase;

    private float pointerPosAngle;

    // Use this for initialization
    void Start () {
        Vector3[] baseCorner = new Vector3[4];
        qTBase.rectTransform.GetWorldCorners(baseCorner);
        hitZone.rectTransform.sizeDelta = new Vector2(Random.Range(5, 11) * 10, hitZone.rectTransform.sizeDelta.y);
        hitZone.rectTransform.position = new Vector2(Random.Range(baseCorner[0].x + hitZone.rectTransform.rect.width / 2 + 50f, baseCorner[2].x - hitZone.rectTransform.rect.width / 2 - 50f), hitZone.rectTransform.position.y);
        pointerPosAngle = 0;
        Vector3[] hitZoneCorner = new Vector3[4];
        
        hitZone.rectTransform.GetWorldCorners(hitZoneCorner);
        if (hitZone != null)
        {
            pointerHitZoneMin = hitZoneCorner[0].x - 50f;
            pointerHitZoneMax = hitZoneCorner[2].x + 50f;
        }
    }
	
	// Update is called once per frame
	void Update () {
        PointerMove();
    }

    void PointerMove()
    {
        pointer.transform.localPosition = new Vector3(Mathf.Sin(pointerPosAngle) * pointerLimit, pointer.transform.localPosition.y, pointer.transform.localPosition.z);
        pointerPosAngle += Time.fixedDeltaTime * pointerSpeed;
    }

    public int PointerHit()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (pointer.transform.position.x > pointerHitZoneMin && pointer.transform.position.x < pointerHitZoneMax)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        return -1;
    }
}