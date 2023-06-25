using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform cam;
    public float relative = 0.3f;
    public bool lockY = false;
    private float lenght;
    private float startpos;

    private void Start()
    {
        cam = FindObjectOfType<CinemachineVirtualCamera>().transform;
        startpos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    // Update is called once per frame
    void Update()
    {
        //if(lockY)
        //{
        //    transform.position = new Vector2(cam.position.x*relative, transform.position.y);
        //}
        //else
        //{
        //    transform.position = new Vector2(cam.position.x * relative, cam.position.y* relative);

        //}
    }

    private void FixedUpdate()
    {
        float temp = (cam.position.x * (1 - relative));
        float dist = (cam.position.x * relative);
        transform.position = new Vector2(startpos + dist, (cam.position.y -8f)* 0.7f);
        
        if (temp > startpos + lenght) {
            startpos += lenght;
        }else if(temp < startpos - lenght)
        {
            startpos -= lenght;
        }
    }
}
