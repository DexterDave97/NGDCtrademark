using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovePiano : MonoBehaviour {

    [SerializeField] GameObject upscene;
    [SerializeField] GameObject Blackie;
    [SerializeField] GameObject Violin;
    public static bool startMoving = false;
    bool locked2 = true, locked = true;
    float time;

    private void Update()
    {
        if (startMoving)
            Move();

        if (!locked && Time.time > time)
        {
            SceneManager.LoadScene("Credits");
        }
    }

    void Move()
    {
        if (upscene.transform.position.y < 20)
            upscene.transform.position = upscene.transform.position + new Vector3(0, 10f * Time.deltaTime, 0);
        Blackie.transform.position = Blackie.transform.position + new Vector3(0, 6f * Time.deltaTime, 0);
        if (transform.position.y < 15.5f && locked2)
            transform.position = transform.position + new Vector3(0, 5f * Time.deltaTime, 0);
        if (upscene.transform.position.y > 20)
            upscene.transform.position = new Vector3(upscene.transform.position.x, 19.9f, upscene.transform.position.z);

        if (transform.position.y >= -15.5 && upscene.transform.position.y == 19.9f)
        {
            locked2 = false;
            transform.position = transform.position - new Vector3(0, 5f * Time.deltaTime, 0);
        }
        else if(!locked2 && transform.position.y <= -15.5 && upscene.transform.position.y == 19.9f)
        {
            Blackie.SetActive(false);
            Violin.SetActive(false);
            startMoving = false;
            if(locked)
            {
                locked = false;
                time = Time.time + 5;
            }
        }
    }
}
